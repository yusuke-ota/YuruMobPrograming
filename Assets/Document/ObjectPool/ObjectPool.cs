using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Document.ObjectPool
{
    public class ObjectPool
    {
        private readonly GameObject _instantiateObject;
        private readonly List<GameObject> _objectPool;
        private readonly Transform _parentTransform;

        // コンストラクタ(MonoBehaviorでは使えない)
        // var objectPool = new MyObjectPool(..); といった形で初期化できるようになる
        public ObjectPool(int amountOfFirstObjects, GameObject instantiateObject, Transform parentTransform)
        {
            _instantiateObject = instantiateObject;
            _parentTransform = parentTransform;
            // 無駄なアロケーションを避けるために、new List()にあらかじめ長さを与えておいた方がよい
            _objectPool = new List<GameObject>(amountOfFirstObjects);
            for (var i = 0; i < amountOfFirstObjects; i++)
                _objectPool.Add(Object.Instantiate(_instantiateObject, _parentTransform));
        }

        /// <summary>
        ///     非アクティブのGameObjectを再利用し、非アクティブのGameObjectがない場合は生成する。
        ///     内部で配列を全探索しているため、高頻度で呼ぶ場合パフォーマンスが良くない。
        /// </summary>
        /// <returns>ObjectPoolが管理しているGameObject</returns>
        public GameObject Rent()
        {
            // objectPool内に使われていないGameObjectがあるか確認し、あったら再利用する
            // _objectPoolを全探索しているので、高頻度で呼ぶ場合パフォーマンスが良くない
            foreach (var pooledObject in _objectPool)
            {
                // 非アクティブなオブジェクトか判別
                if (pooledObject.activeInHierarchy) continue;

                pooledObject.SetActive(true);
                return pooledObject;
            }

            // ないので、生成する
            var gameObject = Object.Instantiate(_instantiateObject, _parentTransform);
            gameObject.SetActive(true);
            _objectPool.Add(gameObject);
            return gameObject;
        }

        // // 非アクティブなGameObjectをキャッシュする配列
        // private List<GameObject> _nonactiveObjectCaches = new List<GameObject>();
        // /// <summary>
        // /// 非アクティブのGameObjectを再利用し、非アクティブのGameObjectがない場合は生成する。
        // /// 非アクティブなGameObjectを配列でキャッシュしているので、高頻度で呼んでもパフォーマンスが劣化しない。
        // /// </summary>
        // /// <returns>ObjectPoolが管理しているGameObject</returns>
        // public GameObject Rent()
        // {
        //     #region キャッシュ用配列が空の時の処理

        //     // キャッシュ用の配列が空の時
        //     if (_nonactiveObjectCaches.Count == 0)
        //     {
        //         // _objectPool内の非アクティブなGameObjectを調べてキャッシュ用配列を作成する
        //         _nonactiveObjectCaches = _objectPool.Where(pooledObject => !pooledObject.activeInHierarchy).ToList();

        //         // 非アクティブなGameObjectの数が0だった時
        //         if (_nonactiveObjectCaches.Count == 0)
        //         {
        //             _nonactiveObjectCaches = new List<GameObject>((int)ObjectPoolAddSize);

        //             // とりあえずいくつかGameObjectを生成して、オブジェクトプールとキャッシュに追加する
        //             for (var unUseIndex = 0; unUseIndex < ObjectPoolAddSize; unUseIndex++)
        //             {
        //                 var instantiate = Object.Instantiate(_instantiateObject, _parentTransform);
        //                 instantiate.SetActive(false);
        //                 _objectPool.Add(instantiate);
        //                 _nonactiveObjectCaches.Add(instantiate);
        //             }
        //         }
        //     }

        //     #endregion

        //     // キャッシュ用配列の最後の要素を取り出し、アクティブ化して返す
        //     var cachedGameObject = _nonactiveObjectCaches.Last();
        //     _nonactiveObjectCaches.Remove(cachedGameObject);
        //     cachedGameObject.SetActive(true);

        //     return cachedGameObject;
        // }

        // 他に処理を入れたくなることもあるかもしれないので、専用のメソッドを生やしておこう
        public void Return(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public uint CountActiveObject()
        {
            return (uint) _objectPool.Count(pooledObject => pooledObject.activeInHierarchy);
        }
    }
}