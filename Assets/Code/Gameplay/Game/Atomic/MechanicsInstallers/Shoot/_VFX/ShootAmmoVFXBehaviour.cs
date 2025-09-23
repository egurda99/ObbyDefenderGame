using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender.Weapons;
using UnityEngine;

public sealed class ShootAmmoVFXBehaviour : IEntityInit, IEntityDispose
{
    private IEvent _shootEvent;
    private ParticleSystem _shootFx;
    private readonly WeaponParticlePosition[] _weaponParticlePositions;
    private ReactiveVariable<WeaponType> _weaponType;

    public ShootAmmoVFXBehaviour(WeaponParticlePosition[] weaponParticlePositions)
    {
        _weaponParticlePositions = weaponParticlePositions;
    }


    public void Init(IEntity entity)
    {
        _shootEvent = entity.GetShootEvent();
        _shootFx = entity.GetShootAmmoFX();
        _weaponType = entity.GetWeaponType();

        _weaponType.Subscribe(OnWeaponChanged);
        _shootEvent.Subscribe(OnShootEvent);
    }

    private void OnWeaponChanged(WeaponType weaponType)
    {
        for (var index = 0; index < _weaponParticlePositions.Length; index++)
        {
            var weaponPos = _weaponParticlePositions[index];
            if (weaponPos.WeaponType == weaponType)
            {
                if (_shootFx != null)
                {
                    _shootFx.transform.position = weaponPos.Transform.position;
                    _shootFx.transform.rotation = weaponPos.Transform.rotation;
                }

                return;
            }
        }
    }

    private void OnShootEvent()
    {
        _shootFx.Play();
    }

    public void Dispose(IEntity entity)
    {
        _shootEvent.Unsubscribe(OnShootEvent);
        _weaponType.Unsubscribe(OnWeaponChanged);
    }
}
