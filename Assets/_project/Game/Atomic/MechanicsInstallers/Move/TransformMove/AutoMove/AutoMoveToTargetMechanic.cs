using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

[Serializable]
public class AutoMoveToTargetMechanic : IEntityInstaller
{
    [SerializeField] private Transform _rootTransform;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _stopDistance = 0.1f;
    [SerializeField] private bool _isMoving;

    public void Install(IEntity entity)
    {
        entity.AddRootTransform(_rootTransform);
        entity.AddMoveSpeed(_speed);
        entity.AddTarget(new ReactiveVariable<Transform>());
        entity.AddMoveDirection(new ReactiveVariable<Vector3>());
        entity.AddStopDistance(_stopDistance);


        entity.AddIsMoving(_isMoving);

        var canMove = new AndExpression();
        entity.AddCanMove(canMove);

        entity.AddBehaviour(new AutoMoveToTargetBehaviour());
    }
}
