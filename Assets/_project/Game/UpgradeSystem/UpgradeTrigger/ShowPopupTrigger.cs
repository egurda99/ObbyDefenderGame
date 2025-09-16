using Atomic.Entities;
using MyCodeBase;
using UnityEngine;
using Zenject;

namespace ObbyDefender.Scripts
{
    public sealed class ShowPopupTrigger : MonoBehaviour
    {
        [SerializeField] private PopupName _popupName;

        private PopupManager _popupManager;

        [Inject]
        public void Construct(PopupManager popupManager)
        {
            _popupManager = popupManager;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    _popupManager.ShowPopup(_popupName);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    _popupManager.HidePopup(_popupName);
                }
            }
        }
    }
}
