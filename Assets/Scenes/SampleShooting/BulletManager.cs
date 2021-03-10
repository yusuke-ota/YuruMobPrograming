using UnityEngine;

namespace Scenes.SampleShooting
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance => _instance;
        private static BulletManager _instance;
        
        [SerializeField] private GameObject bullet;
        [SerializeField] public uint bulletLimit = 20;
        [SerializeField] private Transform playerTransform;
        public ObjectPool.ObjectPool PlayerBulletPool;

        private void Awake()
        {
            _instance = this;
            PlayerBulletPool = new ObjectPool.ObjectPool(bulletLimit, bullet, playerTransform);
        }
    }
}
