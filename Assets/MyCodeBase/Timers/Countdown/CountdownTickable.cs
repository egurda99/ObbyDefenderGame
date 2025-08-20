using Atomic.Elements;
using UnityEngine;
using ITickable = Zenject.ITickable;

namespace MyCodeBase.Timers
{
    public sealed class CountdownTickable : ITickable
    {
        private readonly Countdown _countdown;

        public CountdownTickable(Countdown countdown)
        {
            _countdown = countdown;
        }

        public void Tick()
        {
            _countdown.Tick(Time.deltaTime);
        }
    }
}