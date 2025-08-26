using Atomic.Elements;
using Atomic.Entities;

public sealed class DestroyByLifeTimeBehaviour : IEntityInit, IEntityDisable, IEntityEnable, IEntityUpdate
{
    private ReactiveVariable<float> _lifeTime;
    private AndExpression _canStartTimer;
    private Timer _timer;
    private IEvent _destroyRequest;

    public void Init(IEntity entity)
    {
        _lifeTime = entity.GetLifeTime();
        _canStartTimer = entity.GetCanStartTimer();
        _destroyRequest = entity.GetDestroyRequest();

        _timer = entity.GetLifetimeTimer();
        _timer.SetDuration(_lifeTime.Value);
        _timer.Start();
        _timer.OnEnded += OnTimerEnded;
    }

    private void OnTimerEnded()
    {
        _destroyRequest.Invoke();
        //Object.Destroy(_rootTransform.gameObject);
    }

    private void Reset()
    {
        _timer.Stop();
        _timer.SetDuration(_lifeTime.Value);
        _timer.Start();
    }

    public void Enable(IEntity entity)
    {
        _timer.OnEnded += OnTimerEnded;
        Reset();
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
}
