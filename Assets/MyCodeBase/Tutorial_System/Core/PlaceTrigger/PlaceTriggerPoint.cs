using System;
using UnityEngine;

namespace ObbyDefender.Scripts
{
    public sealed class PlaceTriggerPoint : MonoBehaviour
    {
        public event Action OnPlaceVisited;

        private void OnTriggerEnter(Collider other)
        {
            // if (other.TryGetComponent(out SceneEntityProxy entity))
            // {
            //     if (entity.HasPlayerTag())
            //     {
            //         OnPlaceVisited?.Invoke();
            //     }
            // }
        }
    }
}