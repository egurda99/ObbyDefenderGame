using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class TakeDamageAnimationBehaviour : IEntityInit, IEntityDispose
{
    private static readonly int GotDamage = Animator.StringToHash("GotDamage");
    private Animator _animator;
    private IEvent<float> _gotDamageAction;

    public void Init(IEntity entity)
    {
        _animator = entity.GetAnimator();
        _gotDamageAction = entity.GetTakeDamageAction();
        _gotDamageAction.Subscribe(OnGotDamage);
    }

    private void OnGotDamage(float value)
    {
        _animator.SetTrigger(GotDamage);
    }

    public void Dispose(IEntity entity)
    {
        _gotDamageAction.Unsubscribe(OnGotDamage);
    }
}
