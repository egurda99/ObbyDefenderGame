using UnityEngine;

namespace Elementary
{
    public abstract class ColliderDetectionObserverMonobehaviour : MonoBehaviour
    {
        [SerializeField] private ColliderDetection _sensor;

        protected virtual void OnEnable()
        {
            _sensor.OnCollidersUpdated += OnCollidersUpdated;
        }

        protected virtual void OnDisable()
        {
            _sensor.OnCollidersUpdated -= OnCollidersUpdated;
        }

        protected void OnCollidersUpdated()
        {
            _sensor.GetCollidersUnsafe(out var buffer, out var size);
            OnCollidersUpdated(buffer, size);
        }

        protected abstract void OnCollidersUpdated(Collider[] buffer, int size);
    }
}
