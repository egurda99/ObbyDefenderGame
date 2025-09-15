using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class VisualCoinInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private EnteredTriggerFXMechanic _enteredTriggerFXMechanic;

        public override void Install(IEntity entity)
        {
            _enteredTriggerFXMechanic.Install(entity);
        }
    }
}