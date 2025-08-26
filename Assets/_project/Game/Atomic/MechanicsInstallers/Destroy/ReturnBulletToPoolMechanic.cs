using System;
using Atomic.Entities;
using Zenject;


[Serializable]
public sealed class ReturnBulletToPoolMechanic : IEntityInstaller
{
    private IEntity _entity;
    private IMemoryPool _pool;

    public void Install(IEntity entity)
    {
        _entity = entity;
        _entity.AddBehaviour(new ReturnBulletToPoolBehaviour());
    }

    public void Init(IMemoryPool pool)
    {
        _pool = pool;
        _entity.GetBehaviour<ReturnBulletToPoolBehaviour>().SetPool(pool);
    }
}
