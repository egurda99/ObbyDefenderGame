using Atomic.Entities;
using Elementary;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class MeleeBrainrotAnimalInstaller : SceneEntityInstallerBase, IEnemy
    {
        [SerializeField] private AnimalType _animalType;

        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        [SerializeField] private AutoMoveToTargetByControllerMechanic _autoMoveToTargetByControllerMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;
        [SerializeField] private AutoMeleeAttackMechanic _autoMeleeAttackMechanic;
        [SerializeField] private MeleeReloadMechanic _meleeReloadMechanic;
        [SerializeField] private CheckTargetAliveMechanic _checkTargetAliveMechanic;
        [SerializeField] private SetConcrentTargetMechanic _setConcrentTargetMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;

        // [SerializeField] private DestroyEntityMechanic _destroyEntityMechanic;
        [SerializeField] private SwitchOffCharacterControllerMechanic _switchOffCharacterControllerMechanic;
        [SerializeField] private ReturnAnimalToPoolMechanic _returnAnimalToPoolMechanic;


        private NearestAliveTargetObserver _nearestAliveTargetObserver;
        private readonly int _enemyLayer = 6;
        private readonly int _bulletsLayer = 7;
        private IMemoryPool _pool;
        private IEntity _entity;

        public override void Install(IEntity entity)
        {
            _entity = entity;
            entity.AddEnemyTag();


            _autoMoveToTargetByControllerMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _autoMeleeAttackMechanic.Install(entity);
            _meleeReloadMechanic.Install(entity);
            _checkTargetAliveMechanic.Install(entity);

            _setConcrentTargetMechanic.Install(entity);
            //  _destroyEntityMechanic.Install(entity);

            Physics.IgnoreLayerCollision(_enemyLayer, _bulletsLayer, true);
            _lifeMechanic.Install(entity);
            _switchOffCharacterControllerMechanic.Install(entity);
            _returnAnimalToPoolMechanic.Install(entity);
            _nearestAliveTargetObserver =
                new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanMove().Append(() => !entity.GetIsAttacking().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanAttack().Append(() => !entity.GetIsDead().Value);
            entity.GetCanAttack().Append(() => !entity.GetIsMoving().Value);
            entity.GetCanAttack().Append(() => entity.GetIsTargetAlive().Value);
        }

        public AnimalType AnimalType => _animalType;

        public void OnSpawned(IMemoryPool rangeEnemyPool)
        {
            _pool = rangeEnemyPool;
            Debug.Log("<color=red>OnSpawned</color>");

            ReconfigureEntity();
        }


        public void OnDespawned()
        {
            Debug.Log("<color=orange>OnDeSpawned</color>");
            _colliderDetectionOverlapSphere.Stop();
        }


        private void ReconfigureEntity()
        {
            _entity.GetBehaviour<ReturnAnimalToPoolBehaviour>().SetPool(_pool);
            _colliderDetectionOverlapSphere.Play();
            _entity.GetIsDead().Value = false;
            _entity.GetHitPoints().Value = 40f;
            _entity.GetIsTargetAlive().Value = false;
            _entity.GetIsAttacking().Value = false;
            _entity.GetCharacterController().enabled = true;

            _entity.GetTarget().Value = null;
        }
    }
}
