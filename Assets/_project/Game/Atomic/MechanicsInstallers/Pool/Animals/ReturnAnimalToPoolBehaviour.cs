using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender;
using UnityEngine;
using Zenject;

public sealed class ReturnAnimalToPoolBehaviour : IEntityInit, IEntityEnable, IEntityDisable
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

        var meleeInstaller = _rootTransform.GetComponentInParent<MeleeBrainrotAnimalInstaller>();
        var rangeInstaller = _rootTransform.GetComponentInParent<RangeBrainrotAnimalInstaller>();
        //var installer = _rootTransform.GetComponentInParent<SceneEntityInstallerBase>();
        if (meleeInstaller == null && rangeInstaller == null)
        {
            Debug.LogError("Installer не найден среди родителей");
            return;
        }

        if (meleeInstaller != null)
        {
            _animalPool.Despawn(meleeInstaller);
        }

        if (rangeInstaller != null)
        {
            _animalPool.Despawn(rangeInstaller);
        }
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
