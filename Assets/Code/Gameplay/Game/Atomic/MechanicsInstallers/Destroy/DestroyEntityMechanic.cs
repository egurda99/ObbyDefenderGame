using System;
using Atomic.Entities;

[Serializable]
public sealed class DestroyEntityMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddBehaviour(new DestroyEntityBehaviour());
    }
}