using Atomic.Elements;
using Zenject;

namespace MyCodeBase.Timers
{
    public class TimersInstallerExample : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Countdown + Tickable
            Container.Bind<Countdown>().AsTransient();
            Container.BindInterfacesTo<CountdownTickable>().AsTransient();

            // Timer + Tickable
            Container.Bind<Timer>().AsTransient();
            Container.BindInterfacesTo<TimerTickable>().AsTransient();
        }
    }
}
