using System;
using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender;
using UnityEngine;

[Serializable]
public sealed class AutoShootToTargetMechanic : IEntityInstaller
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private bool _isShooting;
    [SerializeField] private bool _canDamagePlayer;
    private IEntity _entity;


    public void Install(IEntity entity)
    {
        _entity = entity;
        entity.AddFirePoint(_firePoint);
        entity.AddBulletPrefab(_bulletPrefab);


        entity.AddTarget(new ReactiveVariable<Transform>());

        entity.AddShootEvent(new BaseEvent());
        entity.AddShootAction(new BaseEvent());
        entity.AddShootRequest(new BaseEvent());
        entity.AddChangeTargetAction(new BaseEvent<Transform>());

        entity.AddIsShooting(_isShooting);

        var canShoot = new AndExpression();
        entity.AddCanShoot(canShoot);
    }

    public void Init(BulletFactory bulletPool)
    {
        _entity.AddBehaviour(new AutoShootToTargetBehaviour(bulletPool, _canDamagePlayer));
    }
}
