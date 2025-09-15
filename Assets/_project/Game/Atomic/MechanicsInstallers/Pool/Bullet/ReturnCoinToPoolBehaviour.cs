using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender;
using UnityEngine;
using Zenject;

public sealed class ReturnCoinToPoolBehaviour : IEntityInit, IEntityEnable, IEntityDisable
{
    private IEvent _destroyRequest;
    private IMemoryPool _bulletPool;
    private Transform _rootTransform;

    public void Init(IEntity entity)
    {
        _destroyRequest = entity.GetDestroyRequest();
        _rootTransform = entity.GetRootTransform();
    }

    public void SetPool(IMemoryPool pool)
    {
        _bulletPool = pool;
    }

    private void OnDestroyRequested()
    {
        _bulletPool.Despawn(_rootTransform.gameObject.GetComponent<CoinInstaller>());
    }

    public void Enable(IEntity entity)
    {
        _destroyRequest.Subscribe(OnDestroyRequested);
    }

    public void Disable(IEntity entity)
    {
        _destroyRequest.Unsubscribe(OnDestroyRequested);
    }
}
