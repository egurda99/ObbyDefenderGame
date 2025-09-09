using System;
using Atomic.Entities;
using Zenject;

[Serializable]
public sealed class ReturnMeleeAnimalToPoolMechanic : IEntityInstaller
{
    private IEntity _entity;
    private IMemoryPool _pool;

    public void Install(IEntity entity)
    {
        _entity = entity;
        _entity.AddBehaviour(new ReturnMeleeAnimalToPoolBehaviour());
    }

    public void Init(IMemoryPool pool)
    {
        _pool = pool;
        _entity.GetBehaviour<ReturnMeleeAnimalToPoolBehaviour>().SetPool(pool);
    }
}
