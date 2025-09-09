using System;
using Atomic.Entities;
using Elementary;
using ObbyDefender.Weapons;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class RangeBrainrotAnimalInstaller : SceneEntityInstallerBase, IEnemy
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        [SerializeField] private AutoMoveToTargetByControllerMechanic _autoMoveToTargetByControllerMechanic;

        [SerializeField] private RotateToMoveDirectionWhenNoTargetMechanic _rotateToMoveDirectionMechanic;

        [SerializeField] private RotateToTargetMechanic _rotateToTargetMechanic;

        [SerializeField] private AutoShootToTargetMechanic _autoShootToTargetMechanic;
        [SerializeField] private ShootReloadMechanic _shootReloadMechanic;


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
            entity.AddEnemyTag();

            _autoMoveToTargetByControllerMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _rotateToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Init(_bulletFactory);
            _shootReloadMechanic.Install(entity);

            _checkTargetAliveMechanic.Install(entity);

            _setConcrentTargetMechanic.Install(entity);
            _destroyEntityMechanic.Install(entity);

            Physics.IgnoreLayerCollision(_enemyLayer, _bulletsLayer, true);
            _lifeMechanic.Install(entity);
            _switchOffCharacterControllerMechanic.Install(entity);
            _nearestAliveTargetObserver =
                new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.AddWeaponType(WeaponType.RangeAnimal);


            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);

            entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
            entity.GetCanShoot().Append(() => entity.GetIsTargetAlive().Value);
        }

        public EnemyType EnemyType => _enemyType;

        public void OnSpawned()
        {
            throw new NotImplementedException();
        }

        public void OnSpawned(MeleeEnemyPool meleeEnemyPool)
        {
            throw new NotImplementedException();
        }

        public void OnDespawned()
        {
            throw new NotImplementedException();
        }
    }
}
