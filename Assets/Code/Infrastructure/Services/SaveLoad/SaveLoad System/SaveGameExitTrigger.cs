using Atomic.Entities;
using UnityEngine;
using Zenject;

namespace ObbyDefender.Scripts
{
    public sealed class SaveGameExitTrigger : MonoBehaviour
    {
        private SaveLoadManager _saveLoadManager;

        [Inject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    _saveLoadManager.Save();
                }
            }
        }
    }
}