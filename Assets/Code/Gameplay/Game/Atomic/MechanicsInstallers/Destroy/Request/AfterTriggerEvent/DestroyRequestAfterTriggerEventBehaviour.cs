using Atomic.Elements;
using Atomic.Entities;

public sealed class DestroyRequestAfterTriggerEventBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _destroyRequest;
    private IEvent _enteredTriggerEvent;


    public void Init(IEntity entity)
    {
        _destroyRequest = entity.GetDestroyRequest();
        _enteredTriggerEvent = entity.GetEnteredTriggerEvent();

        _enteredTriggerEvent.Subscribe(OnTriggerEntered);
    }

    private void OnTriggerEntered()
    {
        _destroyRequest.Invoke();
    }

    public void Dispose(IEntity entity)
    {
        _enteredTriggerEvent.Unsubscribe(OnTriggerEntered);
    }
}
