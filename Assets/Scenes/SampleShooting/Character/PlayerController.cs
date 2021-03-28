using Scenes.SampleShooting.DIContainer;
using UnityEngine;

namespace Scenes.SampleShooting.Character
{
    /// <summary>
    ///     Playerのキー入力時の処理、被弾時の処理を実装するクラス。
    ///     依存関係についてはIPlayerControllableを経由してDependence Resolver.csから設定される。
    /// </summary>
    public class PlayerController : MonoBehaviour, IDamageable, IPlayerControllable
    {
        [SerializeField] [Tooltip("速度の係数")] private float speedCoefficient = 0.2f;

        [SerializeField] private uint life;

        private uint _bulletLimit;
        private ObjectPool.ObjectPool _objectPool;
        private SampleShooterControls _shooterControls;

        /// <summary>
        ///     事前にActionMapから作成したスクリプトをInputSystemを使用するために読み込む。
        /// </summary>
        private void Awake()
        {
            _shooterControls = new SampleShooterControls();
        }

        private void FixedUpdate()
        {
            // (ActionMapから作成したclassのインスタンス).(使いたいActionMap).(使いたいAction).ReadValue<T>()で入力が取れる。
            // 何の入力を取っているか気になる場合は、実体を確認すると良い。(Asset/Scenes/SampleShooting/SampleShooterControls)
            var moveDistance = _shooterControls.Shooting.Move.ReadValue<Vector2>();
            if (moveDistance.sqrMagnitude >= 0.01f) OnMove(moveDistance);

            // triggeredでフィルタリングしないと、ボタン押しっぱなしで連射してしまう。
            if (_shooterControls.Shooting.Shoot.triggered) OnShoot();
        }

        /// <summary>
        /// InputSystemは使用前にEnableにしておく必要がある。
        /// </summary>
        private void OnEnable()
        {
            _shooterControls.Shooting.Enable();
        }

        /// <summary>
        /// InputSystemは使用後にDisableにしておく必要がある。
        /// </summary>
        private void OnDisable()
        {
            _shooterControls.Shooting.Disable();
        }

        private void OnMove(Vector2 moveDistance)
        {
            transform.localPosition += new Vector3(moveDistance.x, 0, moveDistance.y) * speedCoefficient;
        }

        private void OnShoot()
        {
            if (_bulletLimit <= _objectPool.CountActiveObject()) return;

            var bullet = _objectPool.Rent();
            var thisTransform = transform;
            bullet.transform.position = thisTransform.position;
            bullet.transform.rotation = thisTransform.rotation;
            bullet.GetComponent<IBulletConstractable>()?.Constructor(_objectPool);
        }

        #region IDamageable実装部分

        /// <summary>
        ///     被弾時の処理
        /// </summary>
        public void Damage()
        {
            life -= 1;

            if (life <= 0) gameObject.SetActive(false);
        }
        

        #endregion

        #region IPlayerControllable実装部分

        /// <summary>
        ///     Dependence Resolver.csからDIされる受け口。
        /// </summary>
        /// <param name="playerBalletPool">プレイヤー用のBullet Pool</param>
        public void Construct(IPlayerBalletPool playerBalletPool)
        {
            _objectPool = playerBalletPool.Pool();
            _bulletLimit = playerBalletPool.BulletLimit();
        }

        #endregion
    }
}