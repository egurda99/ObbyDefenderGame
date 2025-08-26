using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender;
using UnityEngine;
using Zenject;

public sealed class DestroyBulletByLifeTimeBehaviour : IEntityInit, IEntityDisable, IEntityUpdate, IEntityEnable
{
    private ReactiveVariable<float> _lifeTime;
    private Transform _rootTransform;
    private AndExpression _canStartTimer;
    private Timer _timer;
    private IMemoryPool _bulletPool;

    public void Init(IEntity entity)
    {
        _lifeTime = entity.GetLifeTime();
        _rootTransform = entity.GetRootTransform();
        _canStartTimer = entity.GetCanStartTimer();

        _timer = entity.GetLifetimeTimer();
        _timer.SetDuration(_lifeTime.Value);
        _timer.Start();
    }

    public void SetPool(IMemoryPool pool)
    {
        _bulletPool = pool;
    }

    private void Reset()
    {
        _timer.Stop();
        _timer.SetDuration(_lifeTime.Value);
        _timer.Start();
    }

    private void OnTimerEnded()
    {
        _bulletPool.Despawn(_rootTransform.gameObject.GetComponent<BulletInstaller>());
    }


    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (_canStartTimer.Value)
        {
            _timer.Tick(deltaTime);
        }
    }

    public void Disable(IEntity entity)
    {
        _timer.OnEnded -= OnTimerEnded;
    }

    public void Enable(IEntity entity)
    {
        _timer.OnEnded += OnTimerEnded;
        Reset();

        if (_bulletPool == null)
            Debug.LogError($"{entity} has no pool set!");
    }
}
