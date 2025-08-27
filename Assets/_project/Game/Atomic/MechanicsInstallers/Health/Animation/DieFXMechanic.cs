using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class DieFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _dieFX;

    public void Install(IEntity entity)
    {
        entity.AddDieFX(_dieFX);

        entity.AddBehaviour(new DieFXBehaviour());
    }
}
