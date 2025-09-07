using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

public sealed class DealDamageByTriggerBehaviour : IEntityInit, IEntityDispose
{
    private TriggerEventDispatcher _triggerEventDispatcher;
    private ReactiveVariable<float> _damage;
    private readonly bool _canDamagePlayer;
    private IEvent _destroyRequest;

    public DealDamageByTriggerBehaviour(bool canDamagePlayer)
    {
        _canDamagePlayer = canDamagePlayer;
    }


    public void Init(IEntity entity)
    {
        _triggerEventDispatcher = entity.GetTriggerEventDispatcher();
        _damage = entity.GetAttackDamage();
        _destroyRequest = entity.GetDestroyRequest();

        _triggerEventDispatcher.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SceneEntityProxy entity))
        {
            if (!_canDamagePlayer && entity.HasPlayerTag())
            {
                return;
            }

            entity.GetTakeDamageAction().Invoke(_damage.Value);
            Debug.Log("<color=red>Deal damage by bullet</color>");
            _destroyRequest.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _triggerEventDispatcher.OnTriggerEntered -= OnTriggerEntered;
    }
}
