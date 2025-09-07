using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class AutoMoveToTargetByControllerBehaviour : IEntityInit, IEntityUpdate
{
    private CharacterController _controller;
    private Transform _root;
    private ReactiveVariable<float> _speed;
    private AndExpression _canMove;
    private ReactiveVariable<bool> _isMoving;
    private ReactiveVariable<Transform> _target;
    private ReactiveVariable<Vector3> _moveDirection;
    private ReactiveVariable<float> _stopDistance;
    private float _fixedY;

    public void Init(IEntity entity)
    {
        _root = entity.GetRootTransform();
        _controller = entity.GetCharacterController();
        _fixedY = _root.position.y;

        _speed = entity.GetMoveSpeed();
        _target = entity.GetTarget();
        _isMoving = entity.GetIsMoving();
        _canMove = entity.GetCanMove();
        _moveDirection = entity.GetMoveDirection();
        _stopDistance = entity.GetStopDistance();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canMove.Value && _target.Value != null)
        {
            // вычисляем направление только по горизонтали
            var worldDirection = _target.Value.position - _root.position;
            worldDirection.y = 0f;

            var sqrDistance = worldDirection.sqrMagnitude;
            var stopThresholdSqr = _stopDistance.Value * _stopDistance.Value;

            if (sqrDistance > stopThresholdSqr)
            {
                _isMoving.Value = true;
                _moveDirection.Value = worldDirection.normalized;

                // движение через CharacterController
                var move = worldDirection.normalized * _speed.Value * deltaTime;
                move.y = 0f; // запрет вертикального смещения
                _controller.Move(move);

                // возвращаем фиксированную Y координату
                var pos = _root.position;
                pos.y = _fixedY;
                _root.position = pos;
            }
            else
            {
                _isMoving.Value = false;
                _moveDirection.Value = Vector3.zero;
            }
        }
        else
        {
            _isMoving.Value = false;
            _moveDirection.Value = Vector3.zero;
        }
    }
}
