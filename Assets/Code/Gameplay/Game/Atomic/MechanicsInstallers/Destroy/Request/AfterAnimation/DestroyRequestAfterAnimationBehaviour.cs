using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;

public sealed class DestroyRequestAfterAnimationBehaviour : IEntityInit, IEntityDispose
{
    private AnimationEventDispatcher _animationEventDispatcher;

    private IEvent _destroyRequest;

    public void Init(IEntity entity)
    {
        _animationEventDispatcher = entity.GetAnimationEventDispatcher();

        _destroyRequest = entity.GetDestroyRequest();

        _animationEventDispatcher.OnEventReceived += OnEventReceived;
    }


    private void OnEventReceived(string eventName)
    {
        if (eventName == "Ended")
        {
            _destroyRequest.Invoke();
        }
    }


    public void Dispose(IEntity entity)
    {
        _animationEventDispatcher.OnEventReceived -= OnEventReceived;
    }
}
