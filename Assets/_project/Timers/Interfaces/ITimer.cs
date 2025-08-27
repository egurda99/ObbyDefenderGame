namespace MyCodeBase.Timers
{
    public interface ITimer :
        IStartable,
        IStoppable,
        IPlayable,
        IPausable,
        IResumable,
        IProgressable,
        IEndable,
        ITickable
    {
    }
}
