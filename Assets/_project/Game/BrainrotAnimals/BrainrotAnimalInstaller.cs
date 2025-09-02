using Atomic.Entities;
using Elementary;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BrainrotAnimalInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ColliderDetectionOverlapSphere _colliderDetectionOverlapSphere;

        [SerializeField] private AutoMoveToTargetMechanic _autoMoveToTargetMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;
        [SerializeField] private AutoMeleeAttackMechanic _autoMeleeAttackMechanic;
        [SerializeField] private MeleeReloadMechanic _meleeReloadMechanic;


        [SerializeField] private LifeMechanic _lifeMechanic;

        //  [SerializeField] private StunMechanic _stunMechanic;

        private NearestTargetObserver _nearestTargetObserver;

        public override void Install(IEntity entity)
        {
            _autoMoveToTargetMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _autoMeleeAttackMechanic.Install(entity);
            _meleeReloadMechanic.Install(entity);


            _lifeMechanic.Install(entity);
            _nearestTargetObserver =
                new NearestTargetObserver(_colliderDetectionOverlapSphere, GetComponent<SceneEntity>());

            entity.GetCanMove().Append(() => !entity.GetIsDead().Value);
            entity.GetCanRotate().Append(() => !entity.GetIsDead().Value);
            // entity.GetCanMove().Append(() => !entity.GetIsStunned().Value);
            //
            // entity.GetCanShoot().Append(() => !entity.GetIsDead().Value);
            //
            // entity.GetCanShoot().Append(() => !entity.GetNeedReload().Value);
            // entity.GetCanShoot().Append(() => !entity.GetIsStunned().Value);
        }
    }
}
