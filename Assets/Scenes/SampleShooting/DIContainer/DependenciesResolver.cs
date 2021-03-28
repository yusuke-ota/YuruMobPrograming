using VContainer.Unity;

namespace Scenes.SampleShooting.DIContainer
{
    public class DependenciesResolver : IInitializable
    {
        private readonly IEnemyBulletPool _enemyBulletPool;
        private readonly Enemies _enemyControllables;
        private readonly IPlayerBalletPool _playerBalletPool;
        private readonly IPlayerControllable _playerControllable;

        public DependenciesResolver(IPlayerBalletPool playerBalletPool, IPlayerControllable playerControllable,
            IEnemyBulletPool enemyBulletPool, Enemies enemyControllableControllables)
        {
            // playerの設定
            _playerBalletPool = playerBalletPool;
            _playerControllable = playerControllable;

            // enemyの設定
            _enemyBulletPool = enemyBulletPool;
            _enemyControllables = enemyControllableControllables;
        }

        public void Initialize()
        {
            _playerControllable.Construct(_playerBalletPool);
            _enemyControllables.Construct(_enemyBulletPool);
        }
    }
}