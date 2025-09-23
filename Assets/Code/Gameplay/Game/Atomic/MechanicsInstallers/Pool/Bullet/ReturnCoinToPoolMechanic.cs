using System;
using Atomic.Entities;
using Zenject;

[Serializable]
public sealed class ReturnCoinToPoolMechanic : IEntityInstaller
{
    private IEntity _entity;
    private IMemoryPool _pool;

    public void Install(IEntity entity)
    {
        _entity = entity;
        _entity.AddBehaviour(new ReturnCoinToPoolBehaviour());
    }

    public void Init(IMemoryPool pool)
    {
        _pool = pool;
        _entity.GetBehaviour<ReturnCoinToPoolBehaviour>().SetPool(_pool);
    }
}
