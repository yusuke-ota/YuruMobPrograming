# ObjectPool

## Instantiate, Destroyは重い

Unityにおけるオブジェクト生成、破棄メソッドObject.Instantiate(..), Object.Destroy(..)はとても重い。

秒間数回程度なら影響は少ないが、短期間に20,30と生成するとフレーム落ちの原因になる。

そうは言っても、シューティングゲームなどでは、弾の生成に大量のオブジェクトが必要。

## 対策

使い終わったオブジェクトを使いまわすことで、Instantiate, Destroyの回数を減らす。

## 実装

簡易的な実装としては以下の通り

```C#
using System.Collections.Generic;
using UnityEngine;

namespace Document.ObjectPool
{
    public class ObjectPool
    {
        private readonly List<GameObject> _objectPool;
        private readonly GameObject _instantiateObject;
        private readonly Transform _parentTransform;

        // コンストラクタ(MonoBehaviorでは使えない)
        // var objectPool = new MyObjectPool(..); といった形で初期化できるようになる
        public ObjectPool(int amountOfFirstObjects, GameObject instantiateObject, Transform parentTransform)
        {
            _instantiateObject = instantiateObject;
            _parentTransform = parentTransform;
            // 無駄なアロケーションを避けるために、new List()にあらかじめ長さを与えておいた方がよい
            _objectPool = new List<GameObject>(amountOfFirstObjects);
            for (var i = 0; i < amountOfFirstObjects; i++) {
                _objectPool.Add(Object.Instantiate(_instantiateObject, _parentTransform));
            }
        }

        /// <summary>
        /// 非アクティブのGameObjectを再利用し、非アクティブのGameObjectがない場合は生成する。
        /// 内部で配列を全探索しているため、高頻度で呼ぶ場合パフォーマンスが良くない。
        /// </summary>
        /// <returns>ObjectPoolが管理しているGameObject</returns>
        public GameObject Rent() {
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
            GameObject gameObject = Object.Instantiate(_instantiateObject, _parentTransform);
            gameObject.SetActive(true);
            _objectPool.Add(gameObject);
            return gameObject;
        }

        // 他に処理を入れたくなることもあるかもしれないので、専用のメソッドを生やしておこう
        public void Return(GameObject gameObject) {
            gameObject.SetActive(false);
        }

        public uint CountActiveObject() => (uint)_objectPool.Count(pooledObject => pooledObject.activeInHierarchy);
    }
}
```
