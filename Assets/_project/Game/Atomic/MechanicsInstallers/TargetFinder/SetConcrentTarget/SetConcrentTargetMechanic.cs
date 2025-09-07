using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class SetConcrentTargetMechanic : IEntityInstaller
{
    [SerializeField] private Transform _target;

    public void Install(IEntity entity)
    {
        entity.AddBehaviour(new SetConcrentTargetBehaviour(_target));
    }
}
