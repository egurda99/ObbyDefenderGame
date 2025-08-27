using System;

namespace MyCodeBase.Timers
{
    public interface IResumable
    {
        event Action OnResumed;
        bool Resume();
    }
}
