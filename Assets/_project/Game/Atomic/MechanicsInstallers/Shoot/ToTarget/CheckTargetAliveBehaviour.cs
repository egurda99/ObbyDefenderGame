using Atomic.Elements;
using Atomic.Entities;

public sealed class CheckTargetAliveBehaviour : IEntityInit, IEntityUpdate
{
    private ReactiveVariable<bool> _isTargetAlive;

    public void Init(IEntity entity)
    {
        _isTargetAlive = entity.GetIsTargetAlive();
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        if (entity.GetTarget() != null && entity.GetTarget().Value != null)
        {
            if ((entity.GetTarget().Value.TryGetComponent(out SceneEntityProxy proxy) && !proxy.GetIsDead().Value) ||
                (entity.GetTarget().Value.TryGetComponent(out SceneEntity sceneEntity) &&
                 !sceneEntity.GetIsDead().Value))
            {
                _isTargetAlive.Value = true;
            }

            else
            {
                _isTargetAlive.Value = false;
            }
        }
    }
}
