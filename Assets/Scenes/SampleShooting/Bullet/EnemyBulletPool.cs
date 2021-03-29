using Scenes.SampleShooting.DIContainer;
using UnityEngine;
using Utility;

namespace Scenes.SampleShooting.Bullet
{
    public class EnemyBulletPool : MonoBehaviour, IEnemyBulletPool
    {
        [SerializeField] private GameObject enemyBullet;
        [SerializeField] private uint enemyBulletPoolSize = 40;
        private ObjectPool _enemyBulletPool;

        private void Awake()
        {
            _enemyBulletPool = new ObjectPool(enemyBulletPoolSize, enemyBullet, transform);
        }

        #region IEnemyBulletPool実装部分

        public ObjectPool Pool()
        {
            return _enemyBulletPool;
        }

        #endregion
    }
}