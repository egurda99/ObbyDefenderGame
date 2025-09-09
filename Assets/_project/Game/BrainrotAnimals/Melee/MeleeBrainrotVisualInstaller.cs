using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class MeleeBrainrotVisualInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private OneD_MoveAnimationMechanic _moveAnimationMechanic;
        [SerializeField] private MeleeAttackAnimationMechanic _meleeAttackAnimationMechanic;


        [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
        [SerializeField] private DestroyRequestAfterAnimationMechanic _destroyRequestAfterAnimationMechanic;

        [SerializeField] private TakeDamageAnimationMechanic _takeDamageAnimationMechanic;


        [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;


        public override void Install(IEntity entity)
        {
            _moveAnimationMechanic.Install(entity);
            _dieAnimationMechanic.Install(entity);
            _meleeAttackAnimationMechanic.Install(entity);
            _destroyRequestAfterAnimationMechanic.Install(entity);


            _takeDamageVFXMechanic.Install(entity);
            _takeDamageAnimationMechanic.Install(entity);
        }
    }
}
