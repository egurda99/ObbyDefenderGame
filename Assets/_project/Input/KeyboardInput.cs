using System;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class KeyboardInput : IPlayerInput, IInitializable, IDisposable
    {
      public event Action<Vector2> OnMove;
      public event Action OnJump;
      public event Action OnAttack;

        private InputSystem_Actions _inputActions;

        public void Initialize()
        {
            _inputActions = new InputSystem_Actions();

            _inputActions.Enable();

            _inputActions.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
            _inputActions.Player.Move.canceled += ctx => OnMove?.Invoke(Vector2.zero);

            _inputActions.Player.Jump.performed += ctx => OnJump?.Invoke();
            _inputActions.Player.Attack.performed += ctx => OnAttack?.Invoke();
        }

        public void Dispose()
        {
            _inputActions.Disable();
        }
    }
}
