using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public class ObjectPool
    {
        private readonly GameObject _instantiateObject;
        private readonly List<GameObject> _objectPool;

        // _objectPool内の非アクティブなGameObjectが0の時に新規生成するGameObjectの数
        // 1フレームに大量に生成すると、処理落ちするので注意(5に根拠はない)
        private readonly uint _objectPoolGrowSize;
        private readonly Transform _parentTransform;

        // 非アクティブなGameObjectをキャッシュする配列
        private List<GameObject> _nonactiveObjectCaches = new List<GameObject>();

        // コンストラクタ(MonoBehaviorでは使えない)
        // var objectPool = new MyObjectPool(..); といった形で初期化できるようになる
        public ObjectPool(uint capacity, GameObject instantiateObject, Transform parentTransform,
            uint objectPoolGrowSize = 5)
        {
            _instantiateObject = instantiateObject;
            _parentTransform = parentTransform;
            _objectPoolGrowSize = objectPoolGrowSize;
            // 無駄なアロケーションを避けるために、new List()にあらかじめ長さを与えておいた方がよい
            _objectPool = new List<GameObject>((int) capacity);
            for (var i = 0; i < capacity; i++)
            {
                var gameObject = Object.Instantiate(_instantiateObject, _parentTransform);
                gameObject.SetActive(false);
                _objectPool.Add(gameObject);
            }
        }

        /// <summary>
        ///     非アクティブのGameObjectを再利用し、非アクティブのGameObjectがない場合は生成する。
        ///     非アクティブなGameObjectを配列でキャッシュしているので、高頻度で呼んでもパフォーマンスが劣化しない。
        /// </summary>
        /// <returns>ObjectPoolが管理しているGameObject</returns>
        public GameObject Rent()
        {
            #region キャッシュ用配列が空の時の処理

            // キャッシュ用の配列が空の時
            if (_nonactiveObjectCaches.Count == 0)
            {
                // _objectPool内の非アクティブなGameObjectを調べてキャッシュ用配列を作成する
                _nonactiveObjectCaches = _objectPool.Where(pooledObject => !pooledObject.activeInHierarchy).ToList();

                // 非アクティブなGameObjectの数が0だった時
                if (_nonactiveObjectCaches.Count == 0)
                {
                    _nonactiveObjectCaches = new List<GameObject>((int) _objectPoolGrowSize);

                    // とりあえずいくつかGameObjectを生成して、オブジェクトプールとキャッシュに追加する
                    for (var unUseIndex = 0; unUseIndex < _objectPoolGrowSize; unUseIndex++)
                    {
                        var instantiate = Object.Instantiate(_instantiateObject, _parentTransform);
                        instantiate.SetActive(false);
                        _objectPool.Add(instantiate);
                        _nonactiveObjectCaches.Add(instantiate);
                    }
                }
            }

            #endregion

            // キャッシュ用配列の最後の要素を取り出し、アクティブ化して返す
            var cachedGameObject = _nonactiveObjectCaches.Last();
            _nonactiveObjectCaches.Remove(cachedGameObject);
            cachedGameObject.SetActive(true);

            return cachedGameObject;
        }

        // 他に処理を入れたくなることもあるかもしれないので、専用のメソッドを生やしておこう
        public void Return(GameObject rentGameObject)
        {
            rentGameObject.SetActive(false);
        }

        public uint CountActiveObject()
        {
            return (uint) _objectPool.Count(pooledObject => pooledObject.activeInHierarchy);
        }
    }
}