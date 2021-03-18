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
        private float shootWait;
        private void OnEnable()
        {
            _objectPool = BulletManager.Instance.EnemyBulletPool;
            shootWait = 1.0f / shootPerSecond;
            bulletTimer = 0f;
            moveTimer = 0f;
        }

        private float bulletTimer;
        private void Update(){
            bulletTimer += Time.deltaTime;
            if (bulletTimer >= shootWait) {
                this.Shoot();
                bulletTimer -= shootWait;
            }
            this.Move();
        }

        private void Shoot(){
            var bullet = _objectPool.Rent();
            var thisTransform = transform;
            bullet.transform.position = thisTransform.position;
            bullet.transform.rotation = thisTransform.rotation;
        }

        private float moveTimer;
        private void Move(){
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveSpan) {
                dx *= -1;
                dy *= -1;
            }

            bullet.transform.position += new Vector3(dx * Time.deltaTime, 0, dy * Time.deltaTime);
        }

        private void Damage(){
            gameObject.SetActive(false);
        }
    }
}