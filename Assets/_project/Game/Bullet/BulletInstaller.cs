using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class BulletInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private MoveToDirectionMechanic _moveToDirectionMechanic;
        [SerializeField] private DestroyBulletByLifeTimeMechanic _destroyByLifeTimeMechanic;
        [SerializeField] private DealDamageByTriggerMechanic _dealDamageByTriggerMechanic;

        [SerializeField] private RotateToMoveDirectionMechanic _rotateToMoveDirectionMechanic;
        // private BulletPool _pool;
        //
        // [Inject]
        // public void Construct(BulletPool pool)
        // {
        //     _pool = pool;
        // }

        [ShowInInspector] [ReadOnly] private IMemoryPool _pool;

        public void SetPool(IMemoryPool pool)
        {
            Debug.Log("Poll seted");
            _pool = pool;
            //  _destroyByLifeTimeMechanic.Init(_pool);
        }

        public override void Install(IEntity entity)
        {
            _moveToDirectionMechanic.Install(entity);
            _rotateToMoveDirectionMechanic.Install(entity);
            _destroyByLifeTimeMechanic.Install(entity);
            _destroyByLifeTimeMechanic.Init(_pool);

            _dealDamageByTriggerMechanic.Install(entity);

            entity.GetCanStartTimer().Append(() => true);
        }
    }
}
