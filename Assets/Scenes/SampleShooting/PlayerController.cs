using SampleShooting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.SampleShooting
{
    public class PlayerController : MonoBehaviour, SampleShooterControls.IShootingActions, IDamageable
    {
        private SampleShooterControls _shooterControls;
        private void Awake()
        {
            _shooterControls = new SampleShooterControls();
            _shooterControls.Shooting.SetCallbacks(this);
        }

        private ObjectPool.ObjectPool _objectPool;
        private uint _bulletLimit;
        private void OnEnable() => _shooterControls.Shooting.Enable();
        private void OnDisable() => _shooterControls.Shooting.Disable();

        #region IShootingActions実装部分

        [SerializeField, Tooltip("速度の係数")] private float speedCoefficient = 0.2f;
        public void OnMove(InputAction.CallbackContext context)
        {
            var inputAxis = context.ReadValue<Vector2>();
            // 入力が弱い場合(触っていない時など)は動作させない
            if (inputAxis.sqrMagnitude < 0.01) return;

            transform.localPosition += new Vector3(inputAxis.x, 0, inputAxis.y) * speedCoefficient;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (_objectPool is null)
            {
                _objectPool = BulletManager.Instance.PlayerBulletPool;
                _bulletLimit = BulletManager.Instance.BulletLimit;
            }
            var buttonPush = context.ReadValue<float>();
            // 入力が弱い場合(触っていない時など)は動作させない
            if (buttonPush < 0.01) return;
            if (_bulletLimit <= _objectPool.CountActiveObject()) return;

            var bullet = _objectPool.Rent();
            var thisTransform = transform;
            bullet.transform.position = thisTransform.position;
            bullet.transform.rotation = thisTransform.rotation;
        }

        #endregion

        [SerializeField] private uint life;
        public void Damage(){
            life -= 1;

            if (life <= 0) {
                gameObject.SetActive(false);
            }
        }
    }
}
