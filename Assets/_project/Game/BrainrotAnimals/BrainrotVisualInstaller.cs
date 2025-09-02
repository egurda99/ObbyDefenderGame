using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BrainrotVisualInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private OneD_MoveAnimationMechanic _moveAnimationMechanic;

        [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
        [SerializeField] private TakeDamageAnimationMechanic _takeDamageAnimationMechanic;


        [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;
        //  [SerializeField] private StunAnimationMechanic _stunAnimationMechanic;
        //   [SerializeField] private StunVFXMechanic _stunVFXMechanic;


        public override void Install(IEntity entity)
        {
            _moveAnimationMechanic.Install(entity);
            _dieAnimationMechanic.Install(entity);

            _takeDamageVFXMechanic.Install(entity);
            _takeDamageAnimationMechanic.Install(entity);
            //   _stunAnimationMechanic.Install(entity);
            //   _stunVFXMechanic.Install(entity);
        }
    }
}
