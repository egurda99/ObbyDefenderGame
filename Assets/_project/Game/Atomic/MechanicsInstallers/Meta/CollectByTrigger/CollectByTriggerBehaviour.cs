using Atomic.Elements;
using Atomic.Entities;
using MyCodeBase.Utils;
using UnityEngine;

public sealed class CollectByTriggerBehaviour : IEntityInit, IEntityDispose
{
    private TriggerEventDispatcher _triggerEventDispatcher;

    private IEvent _triggerEnteredEvent;
    private ReactiveVariable<int> _pointsValue;

    public void Init(IEntity entity)
    {
        _pointsValue = entity.GetPointsValue();
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

            entity.GetCollectedEvent().Invoke();
            entity.GetCollectedPoints().Value += _pointsValue.Value;


            _triggerEnteredEvent?.Invoke();
        }
    }

    public void Dispose(IEntity entity)
    {
        _triggerEventDispatcher.OnTriggerEntered -= OnTriggerEntered;
    }
}
