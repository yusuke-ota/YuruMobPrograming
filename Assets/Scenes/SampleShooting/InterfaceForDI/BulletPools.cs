using Utility;

namespace Scenes.SampleShooting.DIContainer
{
    public interface IEnemyBulletPool
    {
        public ObjectPool Pool();
    }

    public interface IPlayerBulletPool
    {
        ObjectPool Pool();
        uint BulletLimit();
    }
}