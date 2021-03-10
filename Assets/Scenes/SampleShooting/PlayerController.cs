using SampleShooting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.SampleShooting
{
    public class PlayerController : MonoBehaviour, SampleShooterControls.IShootingActions
    {
        private SampleShooterControls _shooterControls;
        private ObjectPool.ObjectPool _objectPool;
        [SerializeField] private GameObject bullet;
        [SerializeField] private uint bulletLimit = 20;
        private void Awake()
        {
            _shooterControls = new SampleShooterControls();
            _shooterControls.Shooting.SetCallbacks(this);
            _objectPool = new ObjectPool.ObjectPool(bulletLimit, bullet, transform);
        }

        private void OnEnable() => _shooterControls.Shooting.Enable();
        private void OnDisable() => _shooterControls.Shooting.Disable();
    
        [SerializeField, Tooltip("1秒あたりの速度減衰率")] private float attenuationRate = 0.1f;
        private void Update()
        {
            _velocity *= 1.0f - attenuationRate * Time.deltaTime;
            transform.localPosition += _velocity * Time.deltaTime;
        }

        #region IShootingActions実装部分

        [SerializeField, Tooltip("速度の係数")] private float speedCoefficient = 0.2f;
        private Vector3 _velocity = Vector3.zero;
        public void OnMove(InputAction.CallbackContext context)
        {
            var inputAxis = context.ReadValue<Vector2>();
            // 入力が弱い場合(触っていない時など)は動作させない
            if (inputAxis.sqrMagnitude < 0.01) return;
        
            _velocity += new Vector3(inputAxis.x, inputAxis.y) * speedCoefficient;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            var buttonPush = context.ReadValue<float>();
            // 入力が弱い場合(触っていない時など)は動作させない
            if (buttonPush < 0.01) return;
            if (bulletLimit <= _objectPool.CountActiveObject()) return;

            _objectPool.Rent();
            // throw new NotImplementedException();
        }
    
        #endregion
    }
}
