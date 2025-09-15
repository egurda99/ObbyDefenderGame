using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class EnteredTriggerFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _destroyFX;
    [SerializeField] [Range(0f, 1f)] private float _procentOfParticleFinished = 0.25f;

    public void Install(IEntity entity)
    {
        entity.AddDestroyFX(_destroyFX);

        entity.AddBehaviour(new EnteredTriggerFXBehaviour(_procentOfParticleFinished));
    }
}
