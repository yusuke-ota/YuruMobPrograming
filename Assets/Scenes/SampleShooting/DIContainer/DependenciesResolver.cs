using VContainer.Unity;

namespace Scenes.SampleShooting.DIContainer
{
    public class DependenciesResolver : IInitializable
    {
        private readonly IEnemyBulletPool _enemyBulletPool;
        private readonly Enemies _enemyControllables;
        private readonly IPlayerBulletPool _playerBulletPool;
        private readonly IPlayerControllable _playerControllable;

        public DependenciesResolver(IPlayerBulletPool playerBulletPool, IPlayerControllable playerControllable,
            IEnemyBulletPool enemyBulletPool, Enemies enemyControllableControllables)
        {
            // playerの設定
            _playerBulletPool = playerBulletPool;
            _playerControllable = playerControllable;

            // enemyの設定
            _enemyBulletPool = enemyBulletPool;
            _enemyControllables = enemyControllableControllables;
        }

        public void Initialize()
        {
            _playerControllable.Construct(_playerBulletPool);
            _enemyControllables.Construct(_enemyBulletPool);
        }
    }
}