using SampleShooting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.SampleShooting
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        private SampleShooterControls _shooterControls;
        private void Awake() => _shooterControls = new SampleShooterControls();
        private void OnEnable() => _shooterControls.Shooting.Enable();
        private void OnDisable() => _shooterControls.Shooting.Disable();
        
        private uint _bulletLimit;
        private ObjectPool.ObjectPool _objectPool;
        [SerializeField, Tooltip("速度の係数")] private float speedCoefficient = 0.2f;
        private void FixedUpdate()
        {
            if (_objectPool is null)
            {
                _objectPool = BulletManager.Instance.PlayerBulletPool;
                _bulletLimit = BulletManager.Instance.BulletLimit;
            }

            var moveDistance = _shooterControls.Shooting.Move.ReadValue<Vector2>();
            if (moveDistance.sqrMagnitude >= 0.01f)
            {
                OnMove(moveDistance);
            }

            var shootKeyInput = _shooterControls.Shooting.Shoot.ReadValue<float>();
            if (shootKeyInput >= InputSystem.settings.defaultButtonPressPoint)
            {
                OnShoot();
            }
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
        }

        [SerializeField] private uint life;
        public void Damage(){
            life -= 1;

            if (life <= 0) {
                gameObject.SetActive(false);
            }
        }
    }
}
