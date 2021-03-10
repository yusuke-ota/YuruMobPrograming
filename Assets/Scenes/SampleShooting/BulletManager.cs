using UnityEngine;

namespace Scenes.SampleShooting
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance { get; private set; }
        public ObjectPool.ObjectPool PlayerBulletPool { get; private set; }
        public uint BulletLimit => bulletLimit;

        [SerializeField] private GameObject bullet;
        [SerializeField] private uint bulletLimit = 20;

        private void Awake()
        {
            Instance = this;
            PlayerBulletPool = new ObjectPool.ObjectPool(bulletLimit, bullet, transform);
        }
    }
}
