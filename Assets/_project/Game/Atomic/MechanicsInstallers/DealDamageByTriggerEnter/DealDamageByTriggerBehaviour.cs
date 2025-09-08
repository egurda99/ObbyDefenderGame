using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

public sealed class DealDamageByTriggerBehaviour : IEntityInit, IEntityDisable, IEntityDispose
{
    private TriggerEventDispatcher _triggerEventDispatcher;
    private ReactiveVariable<float> _damage;
    private ReactiveVariable<bool> _canDamagePlayer;
    private IEvent _destroyRequest;

    public void Init(IEntity entity)
    {
        _triggerEventDispatcher = entity.GetTriggerEventDispatcher();
        _damage = entity.GetAttackDamage();
        _destroyRequest = entity.GetDestroyRequest();
        _canDamagePlayer = entity.GetCanDamagePlayer();

        _triggerEventDispatcher.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SceneEntityProxy entity))
        {
            Debug.Log($"<color=red>_canDamagePlayer : {_canDamagePlayer.Value}</color>");

            if (!_canDamagePlayer.Value && entity.HasPlayerTag())
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

    public void Disable(IEntity entity)
    {
        _canDamagePlayer.Value = false;
    }
}
