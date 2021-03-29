using Scenes.SampleShooting.DIContainer;
using UnityEngine;

namespace Scenes.SampleShooting.Bullet
{
    public class PlayerBulletPool : MonoBehaviour, IPlayerBulletPool
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private uint bulletLimit = 20;
        private ObjectPool.ObjectPool _playerBulletPool;

        private void Awake()
        {
            _playerBulletPool = new ObjectPool.ObjectPool(bulletLimit, bullet, transform);
        }

        #region IPlayerBalletPool実装部分

        public ObjectPool.ObjectPool Pool()
        {
            return _playerBulletPool;
        }

        public uint BulletLimit()
        {
            return bulletLimit;
        }

        #endregion
    }
}