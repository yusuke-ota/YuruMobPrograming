using Scenes.SampleShooting.Bullet;
using Scenes.SampleShooting.Character;
using VContainer;
using VContainer.Unity;

namespace Scenes.SampleShooting.DIContainer
{
    public class SampleSceneGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // DIコンテナにclass, interfaceを登録する。

            // player
            builder.RegisterComponentInHierarchy<PlayerBalletPool>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PlayerController>().AsImplementedInterfaces();

            // enemy
            builder.RegisterComponentInHierarchy<EnemyBulletPool>().AsImplementedInterfaces();
            // todo: シーン内の複数あるゲームオブジェクトに対してDIするにはどうしたらよいのだろうか?
            builder.RegisterComponentInHierarchy<Enemies>();

            // DIコンテナにとうろくされた情報を使って依存性の注入を行う。
            // DIコンテナが活躍するのは、以下のクラスのみで、内部で明示的に依存関係を設定する。
            builder.RegisterEntryPoint<DependenciesResolver>(Lifetime.Scoped);
        }
    }
}
