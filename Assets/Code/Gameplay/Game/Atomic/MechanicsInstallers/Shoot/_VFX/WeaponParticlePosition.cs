using System;
using ObbyDefender.Weapons;
using UnityEngine;

[Serializable]
public sealed class WeaponParticlePosition
{
    [SerializeField] private Transform _transform;
    [SerializeField] private WeaponType _weaponType;

    public Transform Transform => _transform;

    public WeaponType WeaponType => _weaponType;
}
