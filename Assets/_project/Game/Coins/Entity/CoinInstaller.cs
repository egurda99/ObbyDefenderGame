using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class CoinInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private AutoMoveToTargetMechanic _autoMoveToTargetMechanic;
        [SerializeField] private CollectByTriggerMechanic _collectByTriggerMechanic;
        [SerializeField] private DestroyRequestAfterVFXMechanic _destroyRequestAfterVFXMechanic;


        public override void Install(IEntity entity)
        {
            _autoMoveToTargetMechanic.Install(entity);
            _collectByTriggerMechanic.Install(entity);
            _destroyRequestAfterVFXMechanic.Install(entity);
        }
    }
}
