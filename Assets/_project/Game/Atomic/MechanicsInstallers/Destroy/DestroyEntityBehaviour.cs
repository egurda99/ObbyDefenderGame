using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Zenject;

public sealed class DestroyEntityBehaviour : IEntityInit, IEntityEnable, IEntityDisable
{
    private IEvent _destroyRequest;
    private IMemoryPool _bulletPool;
    private Transform _rootTransform;

    public void Init(IEntity entity)
    {
        _destroyRequest = entity.GetDestroyRequest();
        _rootTransform = entity.GetRootTransform();
    }

    private void OnDestroyRequested()
    {
        Object.Destroy(_rootTransform.gameObject);
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