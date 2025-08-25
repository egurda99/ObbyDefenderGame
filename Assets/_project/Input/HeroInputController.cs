using System;
using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender.DI;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class HeroInputController : IDisposable
    {
        private readonly SceneEntity _sceneEntity;

        private IPlayerInput _input;
        private ReactiveVariable<Vector3> _moveDirection;

        public HeroInputController(PlayerService playerService, IPlayerInput input)
        {
            _sceneEntity = playerService.Player;
            _input = input;

            _moveDirection = _sceneEntity.GetMoveDirection();

            _input.OnMove += OnMoveChanged;
        }

        private void OnMoveChanged(Vector2 value)
        {
            _moveDirection.Value = new Vector3(value.x, 0, value.y);
        }

        public void Dispose()
        {
            _input.OnMove -= OnMoveChanged;
        }
    }
}
