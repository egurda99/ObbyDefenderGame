using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Elementary
{
    public sealed class NearestTargetObserver : ColliderDetectionObserver
    {
         private SceneEntity _entity;
         private readonly ReactiveVariable<Transform> _targetVar;

         public NearestTargetObserver(ColliderDetection sensor, SceneEntity entity) : base(sensor)
        {
            _entity = entity;
            _targetVar = new ReactiveVariable<Transform>(null);
            _entity.AddTarget(_targetVar);
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
                _targetVar.Value = nearest;
            }
            else
            {
                if (_targetVar.Value != null)
                {
                    _targetVar.Value = null;
                }
            }
        }
    }
}
