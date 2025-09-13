using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class SetGlobalTargetBehaviour : IEntityInit, IEntityEnable, IEntityDispose
{
    private ReactiveVariable<bool> _isFound;
    private ReactiveVariable<Transform> _target;

    private ReactiveVariable<Transform> _globalTarget;


    public void Init(IEntity entity)
    {
        _target = entity.GetTarget();
        _globalTarget = entity.GetGlobalTarget();

        _target.Subscribe(OnTargetChanged);
    }

    private void OnTargetChanged(Transform value)
    {
        if (value == null && value != _globalTarget.Value)
        {
            _target.Value = _globalTarget.Value;
        }
    }

    public void Dispose(IEntity entity)
    {
        _target.Unsubscribe(OnTargetChanged);
    }

    public void Enable(IEntity entity)
    {
        _target.Value = _globalTarget.Value;
    }
}
