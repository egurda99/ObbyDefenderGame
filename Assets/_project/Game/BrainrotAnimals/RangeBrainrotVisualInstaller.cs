using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class RangeBrainrotVisualInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private OneD_MoveAnimationMechanic _moveAnimationMechanic;
        [SerializeField] private ShootAnimationMechanic _shootAnimationMechanic;


        [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
        [SerializeField] private DestroyRequestAfterAnimationMechanic _destroyRequestAfterAnimationMechanic;

        [SerializeField] private TakeDamageAnimationMechanic _takeDamageAnimationMechanic;


        [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;


        public override void Install(IEntity entity)
        {
            _moveAnimationMechanic.Install(entity);
            _dieAnimationMechanic.Install(entity);
            _shootAnimationMechanic.Install(entity);
            _destroyRequestAfterAnimationMechanic.Install(entity);


            _takeDamageVFXMechanic.Install(entity);
            _takeDamageAnimationMechanic.Install(entity);
        }
    }
}