using System;
using SampleShooting;
using UnityEngine;

namespace Scenes.SampleShooting
{
    public class EnemyController : MonoBehaviour, IDamageable{
        [SerializeField] private int dx;
        [SerializeField] private int dy;
        [SerializeField] private uint moveSpan;

        [SerializeField] private uint shootPerSecond;
        private ObjectPool.ObjectPool _objectPool;
        private float _shootWait;
        private void OnEnable()
        {
            _objectPool = BulletManager.Instance.EnemyBulletPool;
            _shootWait = 1.0f / shootPerSecond;
            _bulletTimer = 0f;
            _moveTimer = 0f;
        }

        private float _bulletTimer;
        private void Update(){
            _bulletTimer += Time.deltaTime;
            if (_bulletTimer >= _shootWait) {
                this.OnShoot();
                _bulletTimer -= _shootWait;
            }
            this.Move();
        }

        private void OnShoot(){
            var bullet = _objectPool.Rent();
            var thisTransform = transform;
            bullet.transform.position = thisTransform.position;
            bullet.transform.rotation = thisTransform.rotation;
        }

        private float _moveTimer;
        private void Move(){
            _moveTimer += Time.deltaTime;
            if (_moveTimer >= moveSpan) {
                dx *= -1;
                dy *= -1;
            }

            transform.position += new Vector3(dx * Time.deltaTime, 0, dy * Time.deltaTime);
        }

        public void Damage(){
            gameObject.SetActive(false);
        }
    }
}