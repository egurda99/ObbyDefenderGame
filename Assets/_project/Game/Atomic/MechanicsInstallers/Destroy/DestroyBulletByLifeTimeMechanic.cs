using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Zenject;

[Serializable]
public sealed class DestroyBulletByLifeTimeMechanic : IEntityInstaller
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Transform _rootTransform;
    private IEntity _entity;


    public void Install(IEntity entity)
    {
        _entity = entity;
        entity.AddLifeTime(_lifeTime);
        entity.AddRootTransform(_rootTransform);
        entity.AddCanStartTimer(new AndExpression());
        entity.AddLifetimeTimer(new Timer());
        _entity.AddBehaviour(new DestroyBulletByLifeTimeBehaviour());
    }

    public void Init(IMemoryPool pool)
    {
        _entity.GetBehaviour<DestroyBulletByLifeTimeBehaviour>().SetPool(pool);
    }
}
