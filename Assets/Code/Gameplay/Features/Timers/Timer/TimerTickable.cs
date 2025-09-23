using Atomic.Elements;
using UnityEngine;
using ITickable = Zenject.ITickable;

namespace MyCodeBase.Timers
{
    public sealed class TimerTickable : Zenject.ITickable
    {
        private readonly Timer _timer;

        public TimerTickable(Timer timer)
        {
            _timer = timer;
        }

        public void Tick()
        {
            _timer.Tick(Time.deltaTime);
        }
    }
}
