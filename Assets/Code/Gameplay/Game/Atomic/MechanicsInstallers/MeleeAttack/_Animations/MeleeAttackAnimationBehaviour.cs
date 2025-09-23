using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

public sealed class MeleeAttackAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private Animator _animator;
    private AnimationEventDispatcher _animationEventDispatcher;
    private IEvent _attackRequsted;
    private IEvent _attackAction;
    private ReactiveVariable<bool> _isAttacking;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _animationEventDispatcher = entity.GetAnimationEventDispatcher();

        _attackRequsted = entity.GetAttackRequest();
        _isAttacking = entity.GetIsAttacking();
        _attackAction = entity.GetAttackAction();

        _attackRequsted.Subscribe(OnAttackRequested);
        _animationEventDispatcher.OnEventReceived += OnEventReceived;
    }


    private void OnEventReceived(string eventName)
    {
        if (eventName == "Attacked")
        {
            _attackAction.Invoke();
        }

        if (eventName == "EndedAttack")
        {
            _isAttacking.Value = false;
            _animator.SetBool(IsAttacking, false);
        }
    }

    private void OnAttackRequested()
    {
        _animator.SetBool(IsAttacking, true);
    }

    public void Dispose(IEntity entity)
    {
        _attackRequsted.Unsubscribe(OnAttackRequested);
        _animationEventDispatcher.OnEventReceived -= OnEventReceived;
    }
}
