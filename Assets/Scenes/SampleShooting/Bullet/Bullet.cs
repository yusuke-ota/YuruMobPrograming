using Scenes.SampleShooting.Character;
using UnityEngine;

namespace Scenes.SampleShooting.Bullet
{
    public class Bullet : MonoBehaviour, IBulletConstractable
    {
        [SerializeField] private float speed = 2;
        [SerializeField] private float lifeTime = 10;

        private float _totalTime;

        private void FixedUpdate()
        {
            _totalTime += Time.deltaTime;
            transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);

            if (_totalTime >= lifeTime) ReturnToObjectPool();
        }

        private void OnEnable()
        {
            _totalTime = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag(other.gameObject.tag)) return;

            other.gameObject.GetComponent<IDamageable>()?.Damage();
            ReturnToObjectPool();
        }

        private void ReturnToObjectPool()
        {
            _bulletPool.Return(gameObject);
        }

        #region IBulletConstractable実装部分

        private ObjectPool.ObjectPool _bulletPool;

        public void Constructor(ObjectPool.ObjectPool playerBulletPool)
        {
            _bulletPool = playerBulletPool;
        }

        #endregion
    }
}