using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _basePosition;


        [Inject] private MeleeEnemyFactory _factory;

        [Button]
        public void SpawnRandomMelee()
        {
            SpawnRandom(_spawnPosition.position);
        }

        public void SpawnRandom(Vector3 position)
        {
            var enemy = _factory.CreateRandom();
            if (enemy != null)
            {
                (enemy as MonoBehaviour).transform.position = position;
                (enemy as MonoBehaviour).GetComponent<SceneEntity>().GetTarget().Value = _basePosition;
            }
        }

        public void SpawnSpecific(EnemyType type, Vector3 position)
        {
            var enemy = _factory.Create(type);
            if (enemy != null)
                (enemy as MonoBehaviour).transform.position = position;
        }
    }
}
