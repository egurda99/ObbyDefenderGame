using System;
using Atomic.Elements;
using Atomic.Entities;

[Serializable]
public sealed class AddToMoneyStorageMechanic : IEntityInstaller
{
    public void Install(IEntity entity)
    {
        entity.AddPointsValue(new ReactiveVariable<int>());

        entity.AddBehaviour(new AddToMoneyStorageBehaviour());
    }
}
