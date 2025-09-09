using Atomic.Entities;
using UnityEngine;

namespace Elementary
{
    public sealed class NearestAliveTargetObserver : ColliderDetectionObserver
    {
        private readonly SceneEntity _entity;

        public NearestAliveTargetObserver(ColliderDetection sensor, SceneEntity entity) : base(sensor)
        {
            _entity = entity;
        }

        protected override void OnCollidersUpdated(Collider[] buffer, int size)
        {
            Debug.Log("Colliders updated " + _entity.Name);
            Transform nearest = null;
            var minDist = float.MaxValue;
            var selfPos = _entity.GetRootTransform().position;
            var targetVar = _entity.GetTarget();

            for (var i = 0; i < size; i++)
            {
                var col = buffer[i];
                if (col == null)
                    continue;

                var tr = col.transform;
                var dist = Vector3.SqrMagnitude(tr.position - selfPos);

                if (IsAliveTarget(tr) && dist < minDist)
                {
                    minDist = dist;
                    nearest = tr;
                }
            }

            targetVar.Value = nearest;
        }

        private static bool IsAliveTarget(Transform tr)
        {
            return (tr.TryGetComponent(out SceneEntity entity) && !entity.GetIsDead().Value) ||
                   (tr.TryGetComponent(out SceneEntityProxy proxy) && !proxy.GetIsDead().Value);
        }
    }
}
