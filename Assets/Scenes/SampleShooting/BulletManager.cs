using UnityEngine;

namespace Scenes.SampleShooting
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance { get; private set; }
        public ObjectPool.ObjectPool PlayerBulletPool { get; private set; }
        public uint BulletLimit => bulletLimit;

        public ObjectPool.ObjectPool EnemyBulletPool { get; private set; }

        [SerializeField] private GameObject bullet;
        [SerializeField] private uint bulletLimit = 20;
        [SerializeField] private GameObject enemyBullet;
        [SerializeField] private uint enemyBulletPoolSize = 40;

        private void Awake()
        {
            Instance = this;
            PlayerBulletPool = new ObjectPool.ObjectPool(bulletLimit, bullet, transform);
            EnemyBulletPool = new ObjectPool.ObjectPool(enemyBulletPoolSize, enemyBullet, transform);
        }
    }
}
