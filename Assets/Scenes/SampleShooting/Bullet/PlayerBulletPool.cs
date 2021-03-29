using Scenes.SampleShooting.DIContainer;
using UnityEngine;
using Utility;

namespace Scenes.SampleShooting.Bullet
{
    public class PlayerBulletPool : MonoBehaviour, IPlayerBulletPool
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private uint bulletLimit = 20;
        private ObjectPool _playerBulletPool;

        private void Awake()
        {
            _playerBulletPool = new ObjectPool(bulletLimit, bullet, transform);
        }

        #region IPlayerBalletPool実装部分

        public ObjectPool Pool()
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