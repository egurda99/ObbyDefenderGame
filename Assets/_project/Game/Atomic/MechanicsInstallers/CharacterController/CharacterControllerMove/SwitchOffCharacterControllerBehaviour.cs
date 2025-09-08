using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class SwitchOffCharacterControllerBehaviour : IEntityInit, IEntityEnable, IEntityDispose
{
    private ReactiveVariable<bool> _isDeath;
    private CharacterController _characterController;

    public void Init(IEntity entity)
    {
        _isDeath = entity.GetIsDead();
        _characterController = entity.GetCharacterController();

        _isDeath.Subscribe(OnIsDeathChanged);
    }

    private void OnIsDeathChanged(bool value)
    {
        if (value)
        {
            _characterController.enabled = false;
        }
    }


    public void Enable(IEntity entity)
    {
        _characterController.enabled = true;
    }

    public void Dispose(IEntity entity)
    {
        _isDeath.Unsubscribe(OnIsDeathChanged);
    }
}