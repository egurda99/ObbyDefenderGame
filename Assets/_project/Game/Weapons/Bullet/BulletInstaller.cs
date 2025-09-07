using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BulletInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;

        [SerializeField] private DestroyRequestByLifeTimeMechanic _destroyRequestByLifeTimeMechanic;

        [SerializeField] private DealDamageByTriggerMechanic _dealDamageByTriggerMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;

        [SerializeField] private ReturnBulletToPoolMechanic _returnBulletToPoolMechanic;

        public override void Install(IEntity entity)
        {
            _moveToDirectionMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _destroyRequestByLifeTimeMechanic.Install(entity);
            _returnBulletToPoolMechanic.Install(entity);

            _dealDamageByTriggerMechanic.Install(entity);

            entity.GetCanStartTimer().Append(() => true);
        }
    }
}
