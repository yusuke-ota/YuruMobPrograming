namespace Scenes.SampleShooting.DIContainer
{
    public interface IEnemyBulletPool
    {
        public ObjectPool.ObjectPool Pool();
    }
    
    public interface IPlayerBalletPool
    {
        ObjectPool.ObjectPool Pool();
        uint BulletLimit();
    }
}