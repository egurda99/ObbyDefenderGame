using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class TurretVisualInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private ShootAnimationMechanic _shootAnimationMechanic;
        [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;

        [SerializeField] private ShootVFXMechanic _shootVFXMechanic;
        [SerializeField] private ShootAmmoVFXMechanic _shootAmmoVFXMechanic;


        [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;


        public override void Install(IEntity entity)
        {
            _shootAnimationMechanic.Install(entity);
            _shootAmmoVFXMechanic.Install(entity);
            _dieAnimationMechanic.Install(entity);
            _shootVFXMechanic.Install(entity);
            _takeDamageVFXMechanic.Install(entity);
        }
    }
}
