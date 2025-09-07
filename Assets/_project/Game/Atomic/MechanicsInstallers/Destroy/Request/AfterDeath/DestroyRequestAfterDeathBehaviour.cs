using Atomic.Elements;
using Atomic.Entities;

public sealed class DestroyRequestAfterDeathBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _destroyRequest;
    private ReactiveVariable<bool> _isDeath;


    public void Init(IEntity entity)
    {
        _destroyRequest = entity.GetDestroyRequest();
        _isDeath = entity.GetIsDead();

        _isDeath.Subscribe(OnIsDeathChanged);
    }

    private void OnIsDeathChanged(bool value)
    {
        if (value)
        {
            _destroyRequest.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _isDeath.Unsubscribe(OnIsDeathChanged);
    }
}