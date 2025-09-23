using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class SetGlobalTargetMechanic : IEntityInstaller
{
    //[SerializeField] private Transform _target;

    public void Install(IEntity entity)
    {
        // if (_target != null)
        // {
        //     entity.AddGlobalTarget(_target);
        // }
        //
        // else
        // {
        //     entity.AddGlobalTarget(null);
        // }

        entity.AddGlobalTarget(new ReactiveVariable<Transform>());
        entity.AddBehaviour(new SetGlobalTargetBehaviour());
    }
}
