using System;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public sealed class ShootAmmoVFXMechanic : IEntityInstaller
{
    [SerializeField] private ParticleSystem _shootAmmoFX;
    [SerializeField] private WeaponParticlePosition[] _weaponParticlePositions;


    public void Install(IEntity entity)
    {
        entity.AddShootAmmoFX(_shootAmmoFX);

        entity.AddBehaviour(new ShootAmmoVFXBehaviour(_weaponParticlePositions));
    }
}
