using System;

namespace MyCodeBase.Timers
{
    public interface IPausable
    {
        event Action OnPaused;

        bool IsPaused();
        bool Pause();
    }
}
