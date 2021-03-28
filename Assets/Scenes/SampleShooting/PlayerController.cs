using SampleShooting;
using UnityEngine;

namespace Scenes.SampleShooting
{
    public interface IPlayerControllable
    {
        public void Construct(IPlayerBalletPool playerBalletPool);
    }
    
    public class PlayerController : MonoBehaviour, IDamageable, IPlayerControllable
    {
        private SampleShooterControls _shooterControls;
        private void Awake() => _shooterControls = new SampleShooterControls();
        private void OnEnable() => _shooterControls.Shooting.Enable();
        private void OnDisable() => _shooterControls.Shooting.Disable();

        public void Construct(IPlayerBalletPool playerBalletPool)
        {
            _objectPool = playerBalletPool.Pool();
            _bulletLimit = playerBalletPool.BulletLimit();
        }
        
        private uint _bulletLimit;
        private ObjectPool.ObjectPool _objectPool;
        [SerializeField, Tooltip("速度の係数")] private float speedCoefficient = 0.2f;
        private void FixedUpdate()
        {
            var moveDistance = _shooterControls.Shooting.Move.ReadValue<Vector2>();
            if (moveDistance.sqrMagnitude >= 0.01f)
            {
                OnMove(moveDistance);
            }

            if (_shooterControls.Shooting.Shoot.triggered)
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
            bullet.GetComponent<Bullet>()?.Constructor(_objectPool);
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
