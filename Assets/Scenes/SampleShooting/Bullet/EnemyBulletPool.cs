using Scenes.SampleShooting.DIContainer;
using UnityEngine;

namespace Scenes.SampleShooting.Bullet
{
    public class EnemyBulletPool : MonoBehaviour, IEnemyBulletPool
    {
        [SerializeField] private GameObject enemyBullet;
        [SerializeField] private uint enemyBulletPoolSize = 40;
        private ObjectPool.ObjectPool _enemyBulletPool;

        private void Awake()
        {
            _enemyBulletPool = new ObjectPool.ObjectPool(enemyBulletPoolSize, enemyBullet, transform);
        }

        #region IEnemyBulletPool実装部分

        public ObjectPool.ObjectPool Pool()
        {
            return _enemyBulletPool;
        }

        #endregion
    }
}