using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

public sealed class AutoShootToTargetBehaviour : IEntityInit, IEntityUpdate, IEntityDispose
{
    private IEvent _shootAction;
    private IEvent _shootEvent;
    private IEvent _shootRequested;


    private IEvent<Transform> _changeTargetAction;

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
        _changeTargetAction = entity.GetChangeTargetAction();

        _changeTargetAction.Subscribe(OnTargetChanged);
        _shootAction.Subscribe(OnShootAction);
    }

    private void OnShootAction()
    {
        var bulletGO = Object.Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

        var bulletEntity = bulletGO.GetComponent<SceneEntity>();

        Debug.Log("Shooted");

        bulletEntity.GetMoveDirection().Value = (_entity.GetTarget().Value.position - _firePoint.position).normalized;
        _shootEvent?.Invoke();

        _isShooting.Value = false;
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canShoot.Value && entity.GetTarget() != null)
        {
            _shootRequested.Invoke();
            _isShooting.Value = true;
        }
    }

    private void OnTargetChanged(Transform target)
    {
      //  _target.Value = target;
    }


    public void Dispose(IEntity entity)
    {
        _changeTargetAction.Unsubscribe(OnTargetChanged);
    }


}
