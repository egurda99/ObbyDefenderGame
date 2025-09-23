using Atomic.Entities;
using UnityEngine;

namespace Elementary
{
    public sealed class CoinsObserver : ColliderDetectionObserver
    {
        private readonly SceneEntity _entity;

        public CoinsObserver(ColliderDetection sensor, SceneEntity entity) : base(sensor)
        {
            _entity = entity;
        }

        protected override void OnCollidersUpdated(Collider[] buffer, int size)
        {
            for (var i = 0; i < size; i++)
            {
                var col = buffer[i];
                if (col == null)
                    continue;

                SetTarget(col.transform);
            }
        }

        private void SetTarget(Transform tr)
        {
            var transform = _entity.GetRootTransform();
            if (tr.TryGetComponent(out SceneEntity entity))
            {
                entity.GetTarget().Value = transform;
            }

            if (tr.TryGetComponent(out SceneEntityProxy proxy))
            {
                proxy.GetTarget().Value = transform;
            }
        }
    }
}
