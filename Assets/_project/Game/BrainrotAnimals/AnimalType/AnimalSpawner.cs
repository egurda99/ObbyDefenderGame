using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class AnimalSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _basePosition;


        private MeleeAnimalFactory _meleeFactory;
        private RangeAnimalFactory _rangeFactory;

        [Inject]
        public void Construct(MeleeAnimalFactory factory, RangeAnimalFactory rangeFactory)
        {
            _rangeFactory = rangeFactory;
            _meleeFactory = factory;
        }


        [Button]
        public void SpawnRandomMelee()
        {
            SpawnRandomMelee(_spawnPosition.position);
        }

        [Button]
        public void SpawnRandomRange()
        {
            SpawnRandomRange(_spawnPosition.position);
        }

        public void SpawnRandomMelee(Vector3 position)
        {
            var enemy = _meleeFactory.CreateRandom();
            if (enemy != null)
            {
                ConfigureEnemy(position, enemy.gameObject);
            }
        }

        [Button]
        public void SpawnSpecificMelee(AnimalType type)
        {
            SpawnSpecificMelee(type, _spawnPosition.position);
        }


        [Button]
        public void SpawnSpecificRange(AnimalType type)
        {
            SpawnSpecificRange(type, _spawnPosition.position);
        }

        public void SpawnSpecificMelee(AnimalType type, Vector3 position)
        {
            var enemy = _meleeFactory.Create(type);
            if (enemy != null)
            {
                ConfigureEnemy(position, enemy.gameObject);
            }
        }


        public void SpawnRandomRange(Vector3 position)
        {
            var enemy = _rangeFactory.CreateRandom();
            if (enemy != null)
            {
                ConfigureEnemy(position, enemy.gameObject);
            }
        }


        public void SpawnSpecificRange(AnimalType type, Vector3 position)
        {
            var enemy = _rangeFactory.Create(type);
            if (enemy != null)
            {
                ConfigureEnemy(position, enemy.gameObject);
            }
        }


        [Button]
        public void SpawnRandomEnemy()
        {
            SpawnRandomEnemy(_spawnPosition.position);
        }

        public void SpawnRandomEnemy(Vector3 position)
        {
            if (Random.value > 0.5f)
                SpawnRandomMelee(position);
            else
                SpawnRandomRange(position);
        }


        private void ConfigureEnemy(Vector3 position, GameObject enemyGO)
        {
            var sceneEntity = enemyGO.GetComponent<SceneEntity>();
            var controller = sceneEntity.GetCharacterController();

            enemyGO.transform.position = position;

            sceneEntity.GetTarget().Value = _basePosition;

            controller.enabled = false;
            sceneEntity.GetRootTransform().position = position;
            controller.enabled = true;
        }
    }
}
