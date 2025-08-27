using Atomic.Entities;
using ObbyDefender.Weapons;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class HeroVisualInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private OneD_MoveAnimationMechanic _moveAnimationMechanic;

        [SerializeField] private ShootAnimationMechanic _shootAnimationMechanic;
        [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
        [SerializeField] private TakeDamageAnimationMechanic _takeDamageAnimationMechanic;

        [SerializeField] private WeaponChangerAnimatorController _weaponChangerAnimatorController;


        [SerializeField] private ShootVFXMechanic _shootVFXMechanic;
        [SerializeField] private ShootAmmoVFXMechanic _shootAmmoVFXMechanic;


        [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;
        //  [SerializeField] private StunAnimationMechanic _stunAnimationMechanic;
        //   [SerializeField] private StunVFXMechanic _stunVFXMechanic;


        public override void Install(IEntity entity)
        {
            _moveAnimationMechanic.Install(entity);
            _shootAnimationMechanic.Install(entity);
            _shootAmmoVFXMechanic.Install(entity);
            _dieAnimationMechanic.Install(entity);

            _shootVFXMechanic.Install(entity);
            _takeDamageVFXMechanic.Install(entity);
            _takeDamageAnimationMechanic.Install(entity);
            //   _stunAnimationMechanic.Install(entity);
            //   _stunVFXMechanic.Install(entity);

            entity.AddWeaponChangerAnimator(_weaponChangerAnimatorController);
        }
    }
}
