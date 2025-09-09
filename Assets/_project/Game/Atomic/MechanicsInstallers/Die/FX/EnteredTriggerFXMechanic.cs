using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class EnteredTriggerFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _destroyFX;

    public void Install(IEntity entity)
    {
        entity.AddDestroyFX(_destroyFX);

        entity.AddBehaviour(new EnteredTriggerFXBehaviour());
    }
}
