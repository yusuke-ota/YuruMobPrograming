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

        private void Update()
        {
            _totalTime += Time.deltaTime;
            if (lifeTime <= _totalTime)
            {
                _manager.PlayerBulletPool.Return(gameObject);
                return;
            }

            transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision){
            if gameObject.CompareTag(collision.gameObject){
                return;
            }

            collision.GetComponent<IDamageable>()?.Damage();
        }
    }
}
