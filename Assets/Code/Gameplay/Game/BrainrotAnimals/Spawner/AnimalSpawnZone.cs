using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [RequireComponent(typeof(Collider))]
    public sealed class AnimalSpawnZone : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly] private bool _isOccupied;

        public Transform SpawnPoint => transform;
        public bool IsOccupied => _isOccupied;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _isOccupied = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                _isOccupied = false;
        }
    }
}
