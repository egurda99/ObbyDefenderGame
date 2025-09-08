using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender;
using ObbyDefender.Weapons;
using UnityEngine;

public sealed class AutoShootToTargetBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _shootAction;
    private IEvent _shootEvent;
    private IEvent _shootRequested;

    private ReactiveVariable<Transform> _firePoint;
    private ReactiveVariable<bool> _isShooting;
    private AndExpression _canShoot;
    private IEntity _entity;

    private readonly BulletFactory _bulletFactory;
    private ReactiveVariable<WeaponType> _weaponType;
    private readonly bool _canDamagePlayer;

    public AutoShootToTargetBehaviour(BulletFactory bulletFactory, bool canDamagePlayer)
    {
        _bulletFactory = bulletFactory;
        _canDamagePlayer = canDamagePlayer;
    }

    public void Init(IEntity entity)
    {
        _entity = entity;
        _shootEvent = entity.GetShootEvent();
        _shootAction = entity.GetShootAction();
        _shootRequested = entity.GetShootRequest();


        _firePoint = entity.GetFirePoint();
        _isShooting = entity.GetIsShooting();
        _canShoot = entity.GetCanShoot();
        _weaponType = entity.GetWeaponType();

        _shootAction.Subscribe(OnShootAction);
    }

    private void OnShootAction()
    {
        var targetVar = _entity.GetTarget();
        if (targetVar == null || targetVar.Value == null)
        {
            return;
        }

        var bullet = _bulletFactory.GetBullet(_weaponType.Value);
        var bulletEntity = bullet.GetComponent<SceneEntity>();

        Debug.Log("Shooted");


        var targetPosition = targetVar.Value.position;
        bulletEntity.GetRootTransform().position = _firePoint.Value.position;
        bulletEntity.GetCanDamagePlayer().Value = _canDamagePlayer;


        bulletEntity.GetMoveDirection().Value = (targetPosition - _firePoint.Value.position).normalized;
        _shootEvent?.Invoke();

        _isShooting.Value = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canShoot.Value && entity.GetTarget() != null && entity.GetTarget().Value != null)
        {
            _shootRequested.Invoke();
            _isShooting.Value = true;
        }
    }

    public void Dispose(IEntity entity)
    {
        _shootAction.Unsubscribe(OnShootAction);
    }
}
