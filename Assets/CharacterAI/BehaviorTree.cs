using System;
using System.Collections.Generic;
using System.Linq;

namespace CharacterAI{
	public interface INode
	{
		BehaviorState Process();
	}

	public interface INodeAddable
	{
		void AddNode(INode node);
		void AddNode(IEnumerable<INode> nodes);
		void RemoveNode(INode node);
		void RemoveNode(IEnumerable<INode> nodes);
	}
	
	public enum BehaviorState
	{
		Success,
		Failer,
		InProgress,
	}

	public static class Utility
	{
		public static void AddNode(List<INode> nodes, INode newNode) =>	nodes.Add(newNode);
		public static void AddNodes(List<INode> nodes, IEnumerable<INode> newNodes) => nodes.AddRange(newNodes);

		public static void RemoveNode(List<INode> nodes, INode removeNode)
		{
			nodes.RemoveAll(removeNode.Equals);
		}
		public static void RemoveNode(List<INode> nodes, IEnumerable<INode> removeNodes)
		{
			nodes.RemoveAll(removeNodes.Contains);
		}

		public static BehaviorState ProcessNodes(IEnumerable<INode> nodes)
		{
			foreach (var node in nodes)
			{
				switch  (node.Process()){
					case BehaviorState.Success:
						continue;
					case BehaviorState.InProgress:
						return BehaviorState.InProgress;
					case BehaviorState.Failer:
						return BehaviorState.Failer;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			
			return BehaviorState.Success;
		}
	}

	public class Repeater : INodeAddable
	{
		private readonly List<INode> _nodes = new List<INode>();
		public void AddNode(INode node) => Utility.AddNode(_nodes, node);
		public void AddNode(IEnumerable<INode> nodes) => Utility.AddNodes(_nodes, nodes);
		public void RemoveNode(INode node) => Utility.RemoveNode(_nodes, node);
		public void RemoveNode(IEnumerable<INode> nodes) => Utility.RemoveNode(_nodes, nodes);
		
		public void Process()
		{
			foreach (var node in _nodes)
			{
				node.Process();
			}
		}
	}

	public class Sequencer: INode, INodeAddable
	{
		private readonly List<INode> _nodes = new List<INode>();
		public void AddNode(INode node) => Utility.AddNode(_nodes, node);
		public void AddNode(IEnumerable<INode> nodes) => Utility.AddNodes(_nodes, nodes);
		public void RemoveNode(INode node) => Utility.RemoveNode(_nodes, node);
		public void RemoveNode(IEnumerable<INode> nodes) => Utility.RemoveNode(_nodes, nodes);

		public BehaviorState Process()
		{
			if (_nodes.Count == 0) return BehaviorState.Success;
			
			return Utility.ProcessNodes(_nodes);
		}
	}

	public class Selector: INode, INodeAddable
	{
		private readonly List<INode> _nodes = new List<INode>();
		public void AddNode(INode node) => Utility.AddNode(_nodes, node);
		public void AddNode(IEnumerable<INode> nodes) => Utility.AddNodes(_nodes, nodes);
		public void RemoveNode(INode node) => Utility.RemoveNode(_nodes, node);
		public void RemoveNode(IEnumerable<INode> nodes) => Utility.RemoveNode(_nodes, nodes);

		public BehaviorState Process(){
			if (_nodes.Count == 0) return BehaviorState.Success;

			var randomIndex = UnityEngine.Random.Range(0, _nodes.Count);
			return _nodes[randomIndex].Process();
		}
	}

	public class ConditionNode: INode, INodeAddable
	{
		private readonly List<INode> _nodes = new List<INode>();
		public void AddNode(INode node) => Utility.AddNode(_nodes, node);
		public void AddNode(IEnumerable<INode> nodes) => Utility.AddNodes(_nodes, nodes);
		public void RemoveNode(INode node) => Utility.RemoveNode(_nodes, node);
		public void RemoveNode(IEnumerable<INode> nodes) => Utility.RemoveNode(_nodes, nodes);
		
		private readonly List<BehaviorState> _staticConditionList = new List<BehaviorState>();
		public void AddStaticCondition(BehaviorState condition){
			_staticConditionList.Add(condition);
		}

		private readonly List<BehaviorState> _dynamicConditionList = new List<BehaviorState>();
		public void AddCondition(ref BehaviorState condition)
		{
			_dynamicConditionList.Add(condition);
		}

		public BehaviorState Process(){
			var currentCondition = _staticConditionList.All(condition => condition == BehaviorState.Success);
			if (!currentCondition) return BehaviorState.Failer;
			if (_dynamicConditionList.Count == 0) return Utility.ProcessNodes(_nodes);
			
			foreach (var dynamicCondition in _dynamicConditionList)
			{
				switch (dynamicCondition)
				{
					case BehaviorState.Success:
						continue;
					case BehaviorState.InProgress:
						return BehaviorState.InProgress;
					case BehaviorState.Failer:
						return BehaviorState.Failer;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return Utility.ProcessNodes(_nodes);
		}
	}

	public class Decorator: INode
	{
		public INode Node { get; set; }

		public BehaviorState DecorateResult { get; set; }

		public BehaviorState Process()
		{
			Node?.Process();

			return DecorateResult;
		}	
	}

	public class ActionNode: INode
	{
		private Func<BehaviorState> _action;
		public void SetAction(Func<BehaviorState> action) => _action = action;

		public BehaviorState Process() => _action?.Invoke() ?? BehaviorState.Success;
	}
}
