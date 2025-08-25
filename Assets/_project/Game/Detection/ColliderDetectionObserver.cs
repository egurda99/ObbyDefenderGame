using System;
using UnityEngine;

namespace Elementary
{
    public abstract class ColliderDetectionObserver : IDisposable
    {

        private readonly ColliderDetection _sensor;

        protected ColliderDetectionObserver(ColliderDetection sensor)
        {
            _sensor = sensor;
            _sensor.OnCollidersUpdated += OnCollidersUpdated;
        }
        protected void OnCollidersUpdated()
        {
            _sensor.GetCollidersUnsafe(out var buffer, out var size);
                OnCollidersUpdated(buffer, size);
        }

        public void Dispose()
        {
            _sensor.OnCollidersUpdated -= OnCollidersUpdated;

        }
        protected abstract void OnCollidersUpdated(Collider[] buffer, int size);

    }
}
