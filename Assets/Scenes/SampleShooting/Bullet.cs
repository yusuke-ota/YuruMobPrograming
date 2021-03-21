using UnityEngine;

namespace Scenes.SampleShooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private float lifeTime = 10;

        private float _totalTime;
        private BulletManager _manager;

        private void Awake()
        {
            _manager = BulletManager.Instance;
        }

        private void OnEnable()
        {
            _totalTime = 0;
        }

        private void FixedUpdate()
        {
            _totalTime += Time.deltaTime;
            transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);

            if (_totalTime >= lifeTime)
            {
                ReturnToObjectPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag(other.gameObject.tag)){
                return;
            }

            other.gameObject.GetComponent<IDamageable>()?.Damage();
            ReturnToObjectPool();
        }

        private void ReturnToObjectPool()
        {
            switch (gameObject.tag)
            {
                case "Player":
                    _manager.PlayerBulletPool.Return(gameObject);
                    return;
                case "Enemy":
                    _manager.EnemyBulletPool.Return(gameObject);
                    return;
            }
        }
    }
}
