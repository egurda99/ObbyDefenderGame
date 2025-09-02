using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class BaseInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private LifeMechanic _lifeMechanic;

        public override void Install(IEntity entity)
        {
            entity.AddPlayerTag();

            entity.AddRootTransform(transform);
            _lifeMechanic.Install(entity);
        }
    }
}
