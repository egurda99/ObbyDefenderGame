using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class SetConcrentTargetBehaviour : IEntityInit, IEntityEnable, IEntityDispose
{
    private ReactiveVariable<bool> _isFound;
    private ReactiveVariable<Transform> _target;

    private readonly Transform _concrentTarget;

    public SetConcrentTargetBehaviour(Transform concrentTarget)
    {
        _concrentTarget = concrentTarget;
    }


    public void Init(IEntity entity)
    {
        _target = entity.GetTarget();

        _target.Subscribe(OnTargetChanged);
    }

    private void OnTargetChanged(Transform value)
    {
        if (value == null && value != _concrentTarget)
        {
            _target.Value = _concrentTarget;
        }
    }

    public void Dispose(IEntity entity)
    {
        _target.Unsubscribe(OnTargetChanged);
    }

    public void Enable(IEntity entity)
    {
        _target.Value = _concrentTarget;
    }
}
