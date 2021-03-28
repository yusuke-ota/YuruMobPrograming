using UnityEngine;

namespace Scenes.SampleShooting.DIContainer
{
    public class Enemies: MonoBehaviour
    {
        private IEnemyBulletPool _enemyBulletPool;
        public void Construct(IEnemyBulletPool enemyBulletPool)
        {
            _enemyBulletPool = enemyBulletPool;
        }
        private void Start()
        {
            var enemies = transform.GetComponentsInChildren<IEnemyControllable>();
            foreach (var enemy in enemies)
            {
                enemy.Construct(_enemyBulletPool);
            }
        }
    }
}
