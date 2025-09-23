using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class CollectVFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _collectedFX;

    public void Install(IEntity entity)
    {
        entity.AddCollectedEvent(new BaseEvent());
        entity.AddCollectFX(_collectedFX);

        entity.AddBehaviour(new CollectVFXBehaviour());
    }
}
