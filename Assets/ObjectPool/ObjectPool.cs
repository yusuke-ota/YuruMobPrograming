using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool
    {
        private readonly List<GameObject> _objectPool;
        private readonly GameObject _instantiateObject;
        private readonly Transform _parentTransform;
        
        // _objectPool内の非アクティブなGameObjectが0の時に新規生成するGameObjectの数
        // 1フレームに大量に生成すると、処理落ちするので注意(5に根拠はない)
        private const uint ObjectPoolAddSize = 5;

        // コンストラクタ(MonoBehaviorでは使えない)
        // var objectPool = new MyObjectPool(..); といった形で初期化できるようになる
        public ObjectPool(uint capacity, GameObject instantiateObject, Transform parentTransform)
        {
            _instantiateObject = instantiateObject;
            _parentTransform = parentTransform;
            // 無駄なアロケーションを避けるために、new List()にあらかじめ長さを与えておいた方がよい
            _objectPool = new List<GameObject>((int)capacity);
            for (var i = 0; i < capacity; i++)
            {
                var gameObject = Object.Instantiate(_instantiateObject, _parentTransform);
                gameObject.SetActive(false);
                _objectPool.Add(gameObject);
            }
        }

        // 非アクティブなGameObjectをキャッシュする配列
        private List<GameObject> _nonactiveObjectCaches = new List<GameObject>();
        /// <summary>
        /// 非アクティブのGameObjectを再利用し、非アクティブのGameObjectがない場合は生成する。
        /// 非アクティブなGameObjectを配列でキャッシュしているので、高頻度で呼んでもパフォーマンスが劣化しない。
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
                    _nonactiveObjectCaches = new List<GameObject>((int)ObjectPoolAddSize);

                    // とりあえずいくつかGameObjectを生成して、オブジェクトプールとキャッシュに追加する
                    for (var unUseIndex = 0; unUseIndex < ObjectPoolAddSize; unUseIndex++)
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
        public void Return(GameObject rentGameObject) {
            rentGameObject.SetActive(false);
        }

        public uint CountActiveObject() => (uint)_objectPool.Count(pooledObject => pooledObject.activeInHierarchy);
    }
}
