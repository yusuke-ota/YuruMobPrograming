using Scenes.SampleShooting.DIContainer;
using UnityEngine;
using Utility;

namespace Scenes.SampleShooting.Character
{
    /// <summary>
    ///     Enemyの毎フレームの動作、被弾時の処理を実装するクラス。
    ///     依存関係についてはIEnemyControllableを経由してDependence Resolver.csから設定される。
    /// </summary>
    public class EnemyController : MonoBehaviour, IDamageable, IEnemyControllable
    {
        [SerializeField] [Tooltip("1秒間に移動する移動量")]
        private int dx;

        [SerializeField] [Tooltip("1秒間に移動する移動量")]
        private int dy;

        [SerializeField] [Tooltip("移動方向が反転するまでの時間(ms)")]
        private uint moveSpan;

        [SerializeField] [Tooltip("弾を発射する間隔(s)")]
        private uint shootPerSecond;

        private ObjectPool _bulletPool;

        private float _bulletTimer;

        private float _moveTimer;

        private void FixedUpdate()
        {
            OnMove();

            _bulletTimer += Time.deltaTime;
            if (_bulletTimer >= shootPerSecond)
            {
                OnShoot();
                _bulletTimer -= shootPerSecond;
            }
        }

        private void OnEnable()
        {
            _bulletTimer = 0f;
            _moveTimer = 0f;
        }

        private void OnShoot()
        {
            var bullet = _bulletPool.Rent();
            var thisTransform = transform;
            bullet.transform.position = thisTransform.position;
            bullet.transform.rotation = thisTransform.rotation;
            bullet.GetComponent<IBulletConstractable>()?.Constructor(_bulletPool);
        }

        private void OnMove()
        {
            _moveTimer += Time.deltaTime;
            if (_moveTimer >= moveSpan)
            {
                dx *= -1;
                dy *= -1;
                _moveTimer -= moveSpan;
            }

            transform.position += new Vector3(dx * Time.deltaTime, 0, dy * Time.deltaTime);
        }
        
        #region IDamageable実装部分

        public void Damage()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region IEnemyControllable実装部分

        public void Construct(IEnemyBulletPool enemyBulletPool)
        {
            _bulletPool = enemyBulletPool.Pool();
        }

        #endregion
    }
}