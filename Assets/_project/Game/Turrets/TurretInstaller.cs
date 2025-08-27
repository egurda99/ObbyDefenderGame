using Atomic.Entities;
using Elementary;
using ObbyDefender.Weapons;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class TurretInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;


        [SerializeField] private RotateToTargetMechanic _rotateToTargetMechanic;

        [SerializeField] private AutoShootToTargetMechanic _autoShootToTargetMechanic;

        [SerializeField] private LifeMechanic _lifeMechanic;

        [SerializeField] private ShootReloadMechanic _shootReloadMechanic;

        private BulletFactory _bulletFactory;
        private NearestTargetObserver _nearestTargetObserver;


        [Inject]
        public void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void Install(IEntity entity)
        {
            _autoShootToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Init(_bulletFactory);
            _rotateToTargetMechanic.Install(entity);

            _lifeMechanic.Install(entity);

            _shootReloadMechanic.Install(entity);

            _nearestTargetObserver =
                new NearestTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.AddWeaponType(WeaponType.M16);


            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);

            entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);
            entity.GetCanShoot().Append(() => !entity.GetIsRotating().Value);
            entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
        }
    }
}
