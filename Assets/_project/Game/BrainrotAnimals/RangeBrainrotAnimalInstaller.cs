using Atomic.Entities;
using Elementary;
using ObbyDefender.Weapons;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class RangeBrainrotAnimalInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        [SerializeField] private AutoMoveToTargetByControllerMechanic _autoMoveToTargetByControllerMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;

        // [SerializeField] private AutoMeleeAttackMechanic _autoMeleeAttackMechanic;
        [SerializeField] private AutoShootToTargetMechanic _autoShootToTargetMechanic;
        [SerializeField] private ShootReloadMechanic _shootReloadMechanic;


        // [SerializeField] private MeleeReloadMechanic _meleeReloadMechanic;
        [SerializeField] private CheckTargetAliveMechanic _checkTargetAliveMechanic;
        [SerializeField] private SetConcrentTargetMechanic _setConcrentTargetMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;
        [SerializeField] private DestroyEntityMechanic _destroyEntityMechanic;
        [SerializeField] private SwitchOffCharacterControllerMechanic _switchOffCharacterControllerMechanic;


        private NearestAliveTargetObserver _nearestAliveTargetObserver;
        private readonly int _enemyLayer = 6;
        private readonly int _bulletsLayer = 7;
        private BulletFactory _bulletFactory;

        [Inject]
        public void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void Install(IEntity entity)
        {
            _autoMoveToTargetByControllerMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            //   _autoMeleeAttackMechanic.Install(entity);
            _autoShootToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Init(_bulletFactory);
            _shootReloadMechanic.Install(entity);
            // _meleeReloadMechanic.Install(entity);
            _checkTargetAliveMechanic.Install(entity);

            _setConcrentTargetMechanic.Install(entity);
            _destroyEntityMechanic.Install(entity);

            Physics.IgnoreLayerCollision(_enemyLayer, _bulletsLayer, true);
            _lifeMechanic.Install(entity);
            _switchOffCharacterControllerMechanic.Install(entity);
            _nearestAliveTargetObserver =
                new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.AddWeaponType(WeaponType.Pistol);


            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);

            entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
            entity.GetCanShoot().Append(() => entity.GetIsTargetAlive().Value);
        }
    }
}
