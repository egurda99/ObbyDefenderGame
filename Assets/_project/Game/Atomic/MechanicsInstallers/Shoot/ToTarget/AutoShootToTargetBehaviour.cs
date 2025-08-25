using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class AutoShootToTargetBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _shootAction;
    private IEvent _shootEvent;
    private IEvent _shootRequested;

    private GameObject _bulletPrefab;
    private Transform _firePoint;
    private ReactiveVariable<bool> _isShooting;
    private AndExpression _canShoot;
    private IEntity _entity;

    public void Init(IEntity entity)
    {
        _entity = entity;
        _shootEvent = entity.GetShootEvent();
        _shootAction = entity.GetShootAction();
        _shootRequested = entity.GetShootRequest();


        _bulletPrefab = entity.GetBulletPrefab();

        _firePoint = entity.GetFirePoint();
        _isShooting = entity.GetIsShooting();
        _canShoot = entity.GetCanShoot();

        _shootAction.Subscribe(OnShootAction);
    }

    private void OnShootAction()
    {
        var bulletGO = Object.Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

        var bulletEntity = bulletGO.GetComponentInChildren<SceneEntity>();

        Debug.Log("Shooted");

        var targetVar = _entity.GetTarget();
        if (targetVar == null || targetVar.Value == null)
        {
            bulletEntity.GetMoveDirection().Value = Vector3.forward;
            _shootEvent?.Invoke();

            _isShooting.Value = false;
            return;
        }

        var targetPosition = targetVar.Value.position;
        bulletEntity.GetMoveDirection().Value = (targetPosition - _firePoint.position).normalized;
        _shootEvent?.Invoke();

        _isShooting.Value = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canShoot.Value && entity.GetTarget() != null && entity.GetTarget() .Value != null)
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
