using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BaseVisualInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private DieAnimationMechanic _dieAnimationMechanic;
        [SerializeField] private DieFXMechanic _dieFXMechanic;

        [SerializeField] private TakeDamageVFXMechanic _takeDamageVFXMechanic;


        public override void Install(IEntity entity)
        {
            _dieAnimationMechanic.Install(entity);
            _dieFXMechanic.Install(entity);
            _takeDamageVFXMechanic.Install(entity);
        }
    }
}
