using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class DieFXBehaviour : IEntityInit, IEntityDispose
{
    private ReactiveVariable<bool> _isDead;
    private ParticleSystem _dieFx;

    public void Init(IEntity entity)
    {
        _dieFx = entity.GetDieFX();
        _isDead = entity.GetIsDead();

        _isDead.Subscribe(OnIsDeadChanged);
    }

    private void OnIsDeadChanged(bool value)
    {
        if (value)
            _dieFx.Play();
    }

    public void Dispose(IEntity entity)
    {
        _isDead.Unsubscribe(OnIsDeadChanged);
    }
}
