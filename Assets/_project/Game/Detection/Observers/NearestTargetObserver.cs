using Atomic.Entities;
using UnityEngine;

namespace Elementary
{
    public sealed class NearestTargetObserver : ColliderDetectionObserver
    {
        private readonly SceneEntity _entity;

        public NearestTargetObserver(ColliderDetection sensor, SceneEntity entity) : base(sensor)
        {
            _entity = entity;
        }

        protected override void OnCollidersUpdated(Collider[] buffer, int size)
        {
            Transform nearest = null;
            var minDist = float.MaxValue;
            var selfPos = _entity.GetRootTransform().position;
            var targetVar = _entity.GetTarget();

            for (var i = 0; i < size; i++)
            {
                var col = buffer[i];
                if (col == null)
                    continue;

                var dist = Vector3.SqrMagnitude(col.transform.position - selfPos);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = col.transform;
                }
            }

            if (nearest != null)
            {
                targetVar.Value = nearest;
            }
            else
            {
                if (targetVar.Value != null)
                {
                    targetVar.Value = null;
                }
            }
        }
    }
}
