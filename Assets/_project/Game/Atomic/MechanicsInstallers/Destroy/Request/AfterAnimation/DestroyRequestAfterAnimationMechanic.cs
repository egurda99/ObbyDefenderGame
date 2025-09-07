using System;
using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

[Serializable]
public sealed class DestroyRequestAfterAnimationMechanic : IEntityInstaller
{
    [SerializeField] private AnimationEventDispatcher _animationEventDispatcher;

    public void Install(IEntity entity)
    {
        entity.AddDestroyRequest(new BaseEvent());

        entity.AddAnimationEventDispatcher(_animationEventDispatcher);

        entity.AddBehaviour(new DestroyRequestAfterAnimationBehaviour());
    }
}
