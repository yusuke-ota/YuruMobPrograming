using UnityEngine;

namespace Document.WhatIsEvent
{
    // 型の定義
    // internalは同じアセンブリ内ではpublic,それ以外ではprivateとして扱う
    internal delegate void MoveDelegate(Vector3 moveAmount);
    internal delegate void DebugLogDelegate(object message);

    public class Delegate: MonoBehaviour
    {
        // 実は回部からも実行できてしまう。
        internal DebugLogDelegate DebugLogDelegate;
        // eventは外部から実行出来ない(関数の追加、削除は可)
        // _moveDelegateに渡す関数はMoveCube.csで作成します。
        internal event MoveDelegate MoveDelegate;

        private static void LogOnUnityEditor(object message)
        {
            Debug.Log(message);
        }

        // _debugLogDelegateはLogOnUnityEditorより先に消えるので、以下のように書かなくてもあまり問題はない(はず)。
        private void OnEnable() => DebugLogDelegate += LogOnUnityEditor;
        private void OnDisable() => DebugLogDelegate -= LogOnUnityEditor;

        private void Update()
        {
            // delegateに何も設定されていないこともあるので、unityで使うときはnullチェックが必要
            // DebugLogDelegateはこのclass内でLogOnUnityEditorを追加しているので無くていいけど...
            if (!(DebugLogDelegate is null)) DebugLogDelegate("呼びました");
            // ちなみにこうも書ける
            // DebugLogDelegate?.Invoke("呼びました");
            
            // ?でnullの場合は何もしない、null出ない場合は処理を進めるということができる。
            // UnityでGetComponent<T>したものがnullなことがある時などに使える。

            // ここではどんな動きをするかわかっていない
            // わかっているのは、Vector3を引数に持つ何らかの関数が呼ばれることだけ
            MoveDelegate?.Invoke(new Vector3(1, 0, 1)); 
        }
    }
}