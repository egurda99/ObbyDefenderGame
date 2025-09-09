using Atomic.Entities;
using Elementary;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BrainrotAnimalInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        [SerializeField] private AutoMoveToTargetByControllerMechanic _autoMoveToTargetByControllerMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;
        [SerializeField] private AutoMeleeAttackMechanic _autoMeleeAttackMechanic;
        [SerializeField] private MeleeReloadMechanic _meleeReloadMechanic;
        [SerializeField] private CheckTargetAliveMechanic _checkTargetAliveMechanic;
        [SerializeField] private SetConcrentTargetMechanic _setConcrentTargetMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;
        [SerializeField] private DestroyEntityMechanic _destroyEntityMechanic;
        [SerializeField] private SwitchOffCharacterControllerMechanic _switchOffCharacterControllerMechanic;


        private NearestAliveTargetObserver _nearestAliveTargetObserver;
        private readonly int _enemyLayer = 6;
        private readonly int _bulletsLayer = 7;

        public override void Install(IEntity entity)
        {
            entity.AddEnemyTag();
            _autoMoveToTargetByControllerMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _autoMeleeAttackMechanic.Install(entity);
            _meleeReloadMechanic.Install(entity);
            _checkTargetAliveMechanic.Install(entity);

            _setConcrentTargetMechanic.Install(entity);
            _destroyEntityMechanic.Install(entity);

            Physics.IgnoreLayerCollision(_enemyLayer, _bulletsLayer, true);
            _lifeMechanic.Install(entity);
            _switchOffCharacterControllerMechanic.Install(entity);
            _nearestAliveTargetObserver =
                new NearestAliveTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanMove().Append(() => !entity.GetIsAttacking().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            entity.GetCanAttack().Append(() => !entity.GetIsDead().Value);
            entity.GetCanAttack().Append(() => !entity.GetIsMoving().Value);
            entity.GetCanAttack().Append(() => entity.GetIsTargetAlive().Value);
        }
    }
}
