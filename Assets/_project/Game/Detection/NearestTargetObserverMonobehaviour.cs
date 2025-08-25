using Atomic.Entities;
using UnityEngine;

namespace Elementary
{
    public sealed class NearestTargetObserver : ColliderDetectionObserver
    {
         private SceneEntity _entity;

        public NearestTargetObserver(ColliderDetection sensor, SceneEntity entity) : base(sensor)
        {
            _entity = entity;
        }

        protected override void OnCollidersUpdated(Collider[] buffer, int size)
        {
            Transform nearest = null;
            float minDist = float.MaxValue;
            var selfPos = _entity.GetRootTransform().position;

            for (int i = 0; i < size; i++)
            {
                var col = buffer[i];
                if (col == null)
                    continue;

                float dist = Vector3.SqrMagnitude(col.transform.position - selfPos);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = col.transform;
                }
            }

            if (nearest != null)
            {
                // обновляем Target через ChangeTargetAction
                // _entity.GetChangeTargetAction().Invoke(nearest);

                   if (_entity.GetTarget() == null)
                   {
                       _entity.AddTarget(nearest);
                       return;
                   }

                    _entity.GetTarget().Value = nearest;

            }
        }
    }
}
