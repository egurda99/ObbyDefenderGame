using System;

namespace MyCodeBase.Timers
{
    public interface IStartable
    {
        event Action OnStarted;
        bool Start();
    }

    public interface IStartable<T>
    {
        event Action<T> OnStarted;
        bool Start(T value);
    }
}
