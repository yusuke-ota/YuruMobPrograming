namespace Scenes.SampleShooting.DIContainer
{
    public interface IEnemyBulletPool
    {
        public ObjectPool.ObjectPool Pool();
    }

    public interface IPlayerBulletPool
    {
        ObjectPool.ObjectPool Pool();
        uint BulletLimit();
    }
}