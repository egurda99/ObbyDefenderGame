using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class CollectVFXBehaviour : IEntityInit, IEntityDispose
{
    private ParticleSystem _collectedFx;
    private IEvent _collectedEvent;


    public void Init(IEntity entity)
    {
        _collectedEvent = entity.GetCollectedEvent();
        _collectedFx = entity.GetCollectFX();

        _collectedEvent.Subscribe(OnCollected);
    }

    private void OnCollected()
    {
        _collectedFx.Play();
    }


    public void Dispose(IEntity entity)
    {
        _collectedEvent.Unsubscribe(OnCollected);
    }
}
