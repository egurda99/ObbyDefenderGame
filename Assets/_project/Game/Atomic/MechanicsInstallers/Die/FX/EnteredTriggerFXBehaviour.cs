using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class EnteredTriggerFXBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _enteredTriggerEvent;
    private ParticleSystem _destroyFX;
    private bool _isStarted;
    private ReactiveVariable<bool> _isFXEnded;

    public void Init(IEntity entity)
    {
        _destroyFX = entity.GetDestroyFX();
        _enteredTriggerEvent = entity.GetEnteredTriggerEvent();
        _isFXEnded = entity.GetIsFXEnded();

        _enteredTriggerEvent.Subscribe(OnTriggerEntered);
    }

    private void OnTriggerEntered()
    {
        _destroyFX.Play();
        _isStarted = true;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_isStarted && !_destroyFX.isPlaying)
        {
            _isFXEnded.Value = true;
            _isStarted = false;
        }
    }

    public void Dispose(IEntity entity)
    {
        _enteredTriggerEvent.Unsubscribe(OnTriggerEntered);
    }
}
