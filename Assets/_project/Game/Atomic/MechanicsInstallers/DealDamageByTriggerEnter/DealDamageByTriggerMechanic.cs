using System;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

[Serializable]
public sealed class DealDamageByTriggerMechanic : IEntityInstaller
{
    [SerializeField] private TriggerEventDispatcher _triggerEventDispatcher;
    [SerializeField] private float _damage;
    [SerializeField] private bool _canDamagePlayer;


    public void Install(IEntity entity)
    {
        entity.AddTriggerEventDispatcher(_triggerEventDispatcher);
        entity.AddAttackDamage(_damage);
        entity.AddCanDamagePlayer(_canDamagePlayer);


        entity.AddBehaviour(new DealDamageByTriggerBehaviour());
    }
}
