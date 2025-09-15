using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class CoinInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private AutoMoveToTargetMechanic _autoMoveToTargetMechanic;

        [SerializeField] private CollectByTriggerMechanic _collectByTriggerMechanic;

        [SerializeField] private DestroyRequestAfterTriggerEventMechanic _destroyRequestAfterTriggerEventMechanic;

        [SerializeField] private ReturnCoinToPoolMechanic _returnCoinToPoolMechanic;
        [SerializeField] private AddToMoneyStorageMechanic _addToMoneyStorageMechanic;

        public override void Install(IEntity entity)
        {
            _autoMoveToTargetMechanic.Install(entity);
            _collectByTriggerMechanic.Install(entity);
            _destroyRequestAfterTriggerEventMechanic.Install(entity);
            _returnCoinToPoolMechanic.Install(entity);
            _addToMoneyStorageMechanic.Install(entity);
        }
    }
}
