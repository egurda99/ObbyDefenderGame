using System;
using Atomic.Elements;
using Atomic.Entities;

[Serializable]
public sealed class DestroyRequestAfterVFXMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddDestroyRequest(new BaseEvent());
        entity.AddIsFXEnded(new ReactiveVariable<bool>());

        entity.AddBehaviour(new DestroyRequestAfterVFXBehaviour());
    }
}
