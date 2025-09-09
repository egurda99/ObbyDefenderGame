using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender;
using UnityEngine;
using Zenject;

public sealed class ReturnMeleeAnimalToPoolBehaviour : IEntityInit, IEntityEnable, IEntityDisable
{
    private IEvent _destroyRequest;
    private IMemoryPool _animalPool;
    private Transform _rootTransform;

    public void Init(IEntity entity)
    {
        _destroyRequest = entity.GetDestroyRequest();
        _rootTransform = entity.GetRootTransform();
    }

    public void SetPool(IMemoryPool pool)
    {
        _animalPool = pool;
    }

    private void OnDestroyRequested()
    {
        if (_animalPool == null)
        {
            Debug.LogError("Pool is NULL! Возможно враг создан не из пула");
            return;
        }

        var installer = _rootTransform.GetComponentInParent<MeleeBrainrotAnimalInstaller>();
        if (installer == null)
        {
            Debug.LogError("MeleeBrainrotAnimalInstaller не найден среди родителей");
            return;
        }

        _animalPool.Despawn(installer);
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
