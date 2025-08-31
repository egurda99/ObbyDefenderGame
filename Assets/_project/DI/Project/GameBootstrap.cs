using MyCodeBase;
using Zenject;

namespace ObbyDefender.DI
{
    public sealed class GameBootstrap : MonoInstaller<GameBootstrap>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
            Container.Bind<MoneyStorage>().AsSingle();
        }
    }
}
