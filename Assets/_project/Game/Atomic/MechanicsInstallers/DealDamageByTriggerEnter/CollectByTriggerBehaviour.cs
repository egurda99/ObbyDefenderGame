using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

public sealed class CollectByTriggerBehaviour : IEntityInit, IEntityDispose
{
    private TriggerEventDispatcher _triggerEventDispatcher;

    private IEvent _triggerEnteredEvent;

    public void Init(IEntity entity)
    {
        _triggerEventDispatcher = entity.GetTriggerEventDispatcher();
        _triggerEnteredEvent = entity.GetEnteredTriggerEvent();

        _triggerEventDispatcher.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider other)
    {
        if (other.TryGetComponent(out SceneEntityProxy entity))
        {
            if (!entity.HasPlayerTag())
            {
                return;
            }

            _triggerEnteredEvent.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _triggerEventDispatcher.OnTriggerEntered -= OnTriggerEntered;
    }
}
