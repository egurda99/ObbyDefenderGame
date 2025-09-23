using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class RotateToMoveDirectionWhenNoTargetBehaviour : IEntityInit, IEntityUpdate
{
    private Transform _rootTransform;
    private AndExpression _canRotate;
    private ReactiveVariable<float> _rotateSpeed;
    private ReactiveVariable<Vector3> _targetDirection;

    private ReactiveVariable<bool> _isRotating;

    private readonly float _minAngleForRotate = 0.5f;
    private ReactiveVariable<Transform> _target;


    public void Init(IEntity entity)
    {
        _rootTransform = entity.GetRootTransform();
        _targetDirection = entity.GetMoveDirection();
        _rotateSpeed = entity.GetRotationSpeed();
        _isRotating = entity.GetIsRotating();
        _canRotate = entity.GetCanRotate();
        _target = entity.GetTarget();
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canRotate.Value)
        {
            var targetVar = entity.GetTarget();
            if (targetVar.Value != null)
                return;


            var direction = _targetDirection.Value;

            if (direction.sqrMagnitude > 0.0001f)
            {
                var newDirection = new Vector3(direction.x, 0f, direction.z);
                var targetRotation = Quaternion.LookRotation(newDirection);
                var angle = Quaternion.Angle(_rootTransform.rotation, targetRotation);

                if (angle > _minAngleForRotate)
                {
                    _isRotating.Value = true;

                    _rootTransform.rotation = Quaternion.RotateTowards(
                        _rootTransform.rotation,
                        targetRotation,
                        _rotateSpeed.Value * deltaTime);
                }
                else
                {
                    _isRotating.Value = false;
                }
            }
            else
            {
                _isRotating.Value = false;
            }
        }
    }
}
