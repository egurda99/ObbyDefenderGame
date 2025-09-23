using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class ShootVFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private WeaponParticlePosition[] _weaponParticlePositions;


    public void Install(IEntity entity)
    {
        entity.AddShootFX(_shootFX);

        entity.AddBehaviour(new ShootVFXBehaviour(_weaponParticlePositions));
    }
}
