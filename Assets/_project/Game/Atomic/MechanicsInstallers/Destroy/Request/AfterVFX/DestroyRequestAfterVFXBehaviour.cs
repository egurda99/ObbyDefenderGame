using Atomic.Elements;
using Atomic.Entities;

public sealed class DestroyRequestAfterVFXBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _destroyRequest;
    private ReactiveVariable<bool> _isFxEnded;

    public void Init(IEntity entity)
    {
        _isFxEnded = entity.GetIsFXEnded();

        _destroyRequest = entity.GetDestroyRequest();

        _isFxEnded.Subscribe(OnIsFXEndedChanged);
    }

    private void OnIsFXEndedChanged(bool value)
    {
        if (value)
        {
            _destroyRequest?.Invoke();
            _isFxEnded.Value = false;
        }
    }


    public void Dispose(IEntity entity)
    {
        _isFxEnded.Unsubscribe(OnIsFXEndedChanged);
    }
}
