using VContainer.Unity;

namespace Scenes.SampleShooting.DIContainer
{
    public class DependenciesResolver : IInitializable
    {
        private readonly IEnemyBulletPool _enemyBulletPool;
        private readonly Enemies _enemies;
        private readonly IPlayerBulletPool _playerBulletPool;
        private readonly IPlayerControllable _playerControllable;

        public DependenciesResolver(IPlayerBulletPool playerBulletPool, IPlayerControllable playerControllable,
            IEnemyBulletPool enemyBulletPool, Enemies enemies)
        {
            // playerの設定
            _playerBulletPool = playerBulletPool;
            _playerControllable = playerControllable;

            // enemyの設定
            _enemyBulletPool = enemyBulletPool;
            _enemies = enemies;
        }

        public void Initialize()
        {
            _playerControllable.Construct(_playerBulletPool);
            _enemies.Construct(_enemyBulletPool);
        }
    }
}