using Atomic.Entities;
using Elementary;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class MeleeBrainrotAnimalInstaller : SceneEntityInstallerBase, IEnemy
    {
        [SerializeField] private EnemyType _enemyType;

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
        [SerializeField] private ReturnMeleeAnimalToPoolMechanic _returnMeleeAnimalToPoolMechanic;


        private NearestAliveTargetObserver _nearestAliveTargetObserver;
        private readonly int _enemyLayer = 6;
        private readonly int _bulletsLayer = 7;
        private MeleeEnemyPool _pool;
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
            _returnMeleeAnimalToPoolMechanic.Install(entity);
            // _nearestAliveTargetObserver =
            //new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanMove().Append(() => !entity.GetIsAttacking().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanAttack().Append(() => !entity.GetIsDead().Value);
            entity.GetCanAttack().Append(() => !entity.GetIsMoving().Value);
            entity.GetCanAttack().Append(() => entity.GetIsTargetAlive().Value);
        }

        public EnemyType EnemyType => _enemyType;

        public void OnSpawned(MeleeEnemyPool meleeEnemyPool)
        {
            _pool = meleeEnemyPool;
            Debug.Log("<color=orange>OnSpawned</color>");
        }

        public void OnRespawned()
        {
            Debug.Log("<color=orange>OnReSpawned</color>");
            _nearestAliveTargetObserver =
                new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());
            _entity.GetIsDead().Value = false;
            _entity.GetHitPoints().Value = 20f;
            _entity.GetIsTargetAlive().Value = false;

            _entity.GetTarget().Value = null;
        }


        public void OnDespawned()
        {
            Debug.Log("<color=orange>OnDeSpawned</color>");
            _nearestAliveTargetObserver?.Dispose();
            _nearestAliveTargetObserver = null;
        }
    }
}
