# ObjectPool

## Instantiate, Destroyは重い

Unityにおけるオブジェクト生成、破棄メソッドObject.Instantiate(..), Object.Destroy(..)はとても重い。

秒間数回程度なら影響は少ないが、短期間に20,30と生成するとフレーム落ちの原因になる。
todo: Gifの貼り付け

そうは言っても、シューティングゲームなどでは、弾の生成に大量のオブジェクトが必要。

## 対策

使い終わったオブジェクトを使いまわすことで、Instantiate, Destroyの回数を減らす。

todo: フロー図

## 実装

簡易的な実装としては以下の通り

```C#
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{

    public class MyObjectPool
    {
        private readonly List<GameObject> _objectPool;
        private readonly GameObject _instantiateObject;
        private readonly Transform _parentTransform;

        // コンストラクタ(MonoBehaviorでは使えない)
        // var objectPool = new MyObjectPool<Type>(..); といった形で初期化できるようになる
        public MyObjectPool(int firstObjects, GameObject instantiateObject, Transform parentTransform)
        {
            _instantiateObject = instantiateObject;
            _parentTransform = parentTransform;
            // 無駄なアロケーションを避けるために、new List()にあらかじめ長さを与えておいた方がよい
            _objectPool = new List<GameObject>(firstObjects);
            for (var i = 0; i < firstObjects; i++) {
                _objectPool.Add(Object.Instantiate(_instantiateObject, _parentTransform));
            }
        }

        public GameObject Rent() {
            // objectPool内に使われていないGameObjectがあるか確認
            // あったら再利用する
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
    }
}
```
