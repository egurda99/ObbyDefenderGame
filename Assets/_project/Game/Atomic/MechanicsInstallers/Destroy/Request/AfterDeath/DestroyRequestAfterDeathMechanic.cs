using System;
using Atomic.Elements;
using Atomic.Entities;

[Serializable]
public sealed class DestroyRequestAfterDeathMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddDestroyRequest(new BaseEvent());
        entity.AddBehaviour(new DestroyRequestAfterDeathBehaviour());
    }
}
