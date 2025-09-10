using System;
using Atomic.Entities;
using Zenject;

[Serializable]
public sealed class ReturnAnimalToPoolMechanic : IEntityInstaller
{
    private IEntity _entity;
    private IMemoryPool _pool;

    public void Install(IEntity entity)
    {
        _entity = entity;
        _entity.AddBehaviour(new ReturnAnimalToPoolBehaviour());
    }
}
