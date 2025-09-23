using Atomic.Entities;

namespace ObbyDefender.DI
{
    public sealed class BaseService
    {
        private SceneEntity _base;

        public SceneEntity Base => _base;

        public BaseService(SceneEntity @base)
        {
            _base = @base;
        }

        public void SetBase(SceneEntity player)
        {
            _base = player;
        }
    }
}
