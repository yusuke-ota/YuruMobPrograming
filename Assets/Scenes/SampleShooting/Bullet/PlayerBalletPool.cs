using Scenes.SampleShooting.DIContainer;
using UnityEngine;

namespace Scenes.SampleShooting.Bullet
{
    public class PlayerBalletPool: MonoBehaviour, IPlayerBalletPool
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private uint bulletLimit = 20;
        private ObjectPool.ObjectPool _playerBulletPool;
        private void Awake()
        {
            _playerBulletPool = new ObjectPool.ObjectPool(bulletLimit, bullet, transform);
        }

        public ObjectPool.ObjectPool Pool() => _playerBulletPool;
        public uint BulletLimit() => bulletLimit;
    }
}