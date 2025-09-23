using Atomic.Entities;
using Elementary;
using ObbyDefender.Weapons;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class RangeBrainrotAnimalInstaller : SceneEntityInstallerBase, IAnimal
    {
        [SerializeField] private AnimalType _animalType;
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        [SerializeField] private AutoMoveToTargetByControllerMechanic _autoMoveToTargetByControllerMechanic;

        [SerializeField] private RotateToMoveDirectionWhenNoTargetMechanic _rotateToMoveDirectionMechanic;

        [SerializeField] private RotateToTargetMechanic _rotateToTargetMechanic;

        [SerializeField] private AutoShootToTargetMechanic _autoShootToTargetMechanic;
        [SerializeField] private ShootReloadMechanic _shootReloadMechanic;


        [SerializeField] private CheckTargetAliveMechanic _checkTargetAliveMechanic;
        [SerializeField] private SetGlobalTargetMechanic _setGlobalTargetMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;

        [SerializeField] private SwitchOffCharacterControllerMechanic _switchOffCharacterControllerMechanic;
        [SerializeField] private ReturnAnimalToPoolMechanic _returnAnimalToPoolMechanic;

        [SerializeField] private int _enemyLayer = 6;
        [SerializeField] private int _bulletsLayer = 7;
        public AnimalType AnimalType => _animalType;


        private NearestAliveTargetObserver _nearestAliveTargetObserver;
        private BulletFactory _bulletFactory;

        private IMemoryPool _pool;
        private IEntity _entity;

        [Inject]
        public void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public override void Install(IEntity entity)
        {
            _entity = entity;
            entity.AddEnemyTag();

            _autoMoveToTargetByControllerMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _rotateToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Install(entity);
            _autoShootToTargetMechanic.Init(_bulletFactory);
            _shootReloadMechanic.Install(entity);

            _checkTargetAliveMechanic.Install(entity);

            _setGlobalTargetMechanic.Install(entity);
            //  _destroyEntityMechanic.Install(entity);

            Physics.IgnoreLayerCollision(_enemyLayer, _bulletsLayer, true);
            _lifeMechanic.Install(entity);
            _switchOffCharacterControllerMechanic.Install(entity);
            _nearestAliveTargetObserver =
                new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            _returnAnimalToPoolMechanic.Install(entity);

            entity.AddWeaponType(WeaponType.RangeAnimal);


            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);

            entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
            entity.GetCanShoot().Append(() => entity.GetIsTargetAlive().Value);
        }


        public void OnSpawned(IMemoryPool rangeEnemyPool)
        {
            _pool = rangeEnemyPool;

            ReconfigureEntity();
        }

        public void OnDespawned()
        {
            _colliderDetectionOverlapSphere.Stop();
        }

        private void ReconfigureEntity()
        {
            _entity.GetBehaviour<ReturnAnimalToPoolBehaviour>().SetPool(_pool);
            _colliderDetectionOverlapSphere.Play();
            _entity.GetIsDead().Value = false;
            _entity.GetIsTargetAlive().Value = false;
            _entity.GetCharacterController().enabled = true;
        }
    }
}
