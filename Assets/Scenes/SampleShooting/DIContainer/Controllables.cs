namespace Scenes.SampleShooting.DIContainer
{
    public interface IPlayerControllable
    {
        public void Construct(IPlayerBalletPool playerBalletPool);
    }
    
    public interface IEnemyControllable
    {
        public void Construct(IEnemyBulletPool enemyBulletPool);
    }

}