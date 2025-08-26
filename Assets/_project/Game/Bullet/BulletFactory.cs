using System;
using ObbyDefender.Weapons;
using Zenject;

namespace ObbyDefender
{
    public sealed class BulletFactory
    {
        private readonly BulletPool _pistolPool;
        private readonly BulletPool _m16Pool;

        public BulletFactory(
            [Inject(Id = WeaponType.Pistol)] BulletPool pistolPool,
            [Inject(Id = WeaponType.M16)] BulletPool m16Pool)
        {
            _pistolPool = pistolPool;
            _m16Pool = m16Pool;
        }

        public BulletInstaller GetBullet(WeaponType type)
        {
            return type switch
            {
                WeaponType.Pistol => _pistolPool.Spawn(),
                WeaponType.M16 => _m16Pool.Spawn(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
