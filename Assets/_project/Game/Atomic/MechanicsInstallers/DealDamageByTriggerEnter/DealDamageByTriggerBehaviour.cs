using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

public sealed class DealDamageByTriggerBehaviour : IEntityInit, IEntityDisable, IEntityDispose
{
    private TriggerEventDispatcher _triggerEventDispatcher;
    private ReactiveVariable<float> _damage;
    private ReactiveVariable<bool> _canDamagePlayer;
    private IEvent _triggerEnteredEvent;

    public void Init(IEntity entity)
    {
        _triggerEventDispatcher = entity.GetTriggerEventDispatcher();
        _damage = entity.GetAttackDamage();
        _triggerEnteredEvent = entity.GetEnteredTriggerEvent();
        _canDamagePlayer = entity.GetCanDamagePlayer();

        _triggerEventDispatcher.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SceneEntityProxy entity))
        {
            if (!_canDamagePlayer.Value && entity.HasHeroTeamTag())
            {
                return;
            }

            if (_canDamagePlayer.Value && entity.HasEnemyTag())
            {
                return;
            }

            if (!entity.GetIsDead().Value)
            {
                entity.GetTakeDamageAction().Invoke(_damage.Value);
            }

            _triggerEnteredEvent.Invoke();
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
