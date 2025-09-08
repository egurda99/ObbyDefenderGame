using System;
using Atomic.Entities;

[Serializable]
public sealed class SwitchOffCharacterControllerMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddBehaviour(new SwitchOffCharacterControllerBehaviour());
    }
}
