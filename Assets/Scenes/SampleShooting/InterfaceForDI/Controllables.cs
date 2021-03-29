namespace Scenes.SampleShooting.DIContainer
{
    public interface IPlayerControllable
    {
        public void Construct(IPlayerBulletPool playerBulletPool);
    }

    public interface IEnemyControllable
    {
        public void Construct(IEnemyBulletPool enemyBulletPool);
    }
}