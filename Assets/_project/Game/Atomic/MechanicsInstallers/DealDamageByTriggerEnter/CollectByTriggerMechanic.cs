using System;
using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

[Serializable]
public sealed class CollectByTriggerMechanic : IEntityInstaller
{
    [SerializeField] private TriggerEventDispatcher _triggerEventDispatcher;

    public void Install(IEntity entity)
    {
        entity.AddTriggerEventDispatcher(_triggerEventDispatcher);
        entity.AddEnteredTriggerEvent(new BaseEvent());


        entity.AddBehaviour(new CollectByTriggerBehaviour());
    }
}
