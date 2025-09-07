using Atomic.Entities;
using Elementary;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BrainrotAnimalInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        //  [SerializeField] private AutoMoveToTargetMechanic _autoMoveToTargetMechanic;
        [SerializeField] private AutoMoveToTargetByControllerMechanic _autoMoveToTargetByControllerMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;
        [SerializeField] private AutoMeleeAttackMechanic _autoMeleeAttackMechanic;
        [SerializeField] private MeleeReloadMechanic _meleeReloadMechanic;
        [SerializeField] private CheckTargetAliveMechanic _checkTargetAliveMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;


        private NearestAliveTargetObserver _nearestAliveTargetObserver;

        public override void Install(IEntity entity)
        {
            // _autoMoveToTargetMechanic.Install(entity);
            _autoMoveToTargetByControllerMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _autoMeleeAttackMechanic.Install(entity);
            _meleeReloadMechanic.Install(entity);
            _checkTargetAliveMechanic.Install(entity);

            Physics.IgnoreLayerCollision(6, 7, true);
            _lifeMechanic.Install(entity);
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
