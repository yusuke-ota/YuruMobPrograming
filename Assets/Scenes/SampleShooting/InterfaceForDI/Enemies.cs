using UnityEngine;

namespace Scenes.SampleShooting.DIContainer
{
    /// <summary>
    ///     シーン内にある複数のEnemyをDIコンテナに上手く登録できなかったので、苦肉の策として、複数のEnemyの情報を保持するクラスを作成。
    ///     Dependence Resolver.csからIEnemyBulletPoolの情報をもらって、シーン内のIEnemyControllableに渡す役割をする。
    ///     todo: もっといい方法があるはずなので、見つけたら書き換え
    /// </summary>
    public class Enemies : MonoBehaviour
    {
        private IEnemyBulletPool _enemyBulletPool;

        private void Start()
        {
            var enemies = transform.GetComponentsInChildren<IEnemyControllable>();
            foreach (var enemy in enemies) enemy.Construct(_enemyBulletPool);
        }

        public void Construct(IEnemyBulletPool enemyBulletPool)
        {
            _enemyBulletPool = enemyBulletPool;
        }
    }
}