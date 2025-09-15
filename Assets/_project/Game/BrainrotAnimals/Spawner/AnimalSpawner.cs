using System;
using System.Collections.Generic;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using ShootEmUp;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObbyDefender
{
    public sealed class AnimalSpawner : IGameStartListener
    {
        private readonly List<AnimalSpawnZone> _zones;
        private readonly Transform _basePosition;
        private readonly MeleeAnimalFactory _meleeFactory;
        private readonly RangeAnimalFactory _rangeFactory;
        private readonly float _minSpawnDelay;
        private readonly float _maxSpawnDelay;
        private readonly AnimalTypesHolder _animalTypesHolder;
        private readonly WavesSystem _wavesSystem;

        public event Action<SceneEntity> OnEnemySpawned;
        public event Action OnWaveSpawnEnded;

        public AnimalSpawner(
            List<AnimalSpawnZone> zones,
            Transform basePosition,
            MeleeAnimalFactory meleeFactory,
            RangeAnimalFactory rangeFactory,
            AnimalTypesHolder animalTypesHolder,
            WavesSystem wavesSystem,
            float minSpawnDelay,
            float maxSpawnDelay)
        {
            _wavesSystem = wavesSystem;
            _animalTypesHolder = animalTypesHolder;
            _zones = zones;
            _basePosition = basePosition;
            _meleeFactory = meleeFactory;
            _rangeFactory = rangeFactory;
            _minSpawnDelay = minSpawnDelay;
            _maxSpawnDelay = maxSpawnDelay;
        }


        public void OnStartGame()
        {
            SpawnWaveAsync(_wavesSystem.GetCurrentWave()).Forget();
        }

        private async UniTask SpawnWaveAsync(Wave wave)
        {
            if (wave == null)
            {
                Debug.LogError("SpawnWaveAsync: wave is null!");
                return;
            }

            if (wave.Enemies == null)
            {
                Debug.LogError("SpawnWaveAsync: wave.Enemies is null!");
                return;
            }

            foreach (var entry in wave.Enemies)
            {
                if (entry == null)
                {
                    Debug.LogWarning("SpawnWaveAsync: entry is null!");
                    continue;
                }

                for (var i = 0; i < entry.Count; i++)
                {
                    SpawnEnemy(entry.EnemyId, entry.Stats);

                    var delay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
                    await UniTask.Delay(TimeSpan.FromSeconds(delay));
                }
            }

            OnWaveSpawnEnded?.Invoke();
        }

        // ? Спавн конкретного врага из волны
        private void SpawnEnemy(AnimalType enemyType, EnemyStats entryStats)
        {
            var zone = GetRandomFreeZone();
            if (zone == null)
            {
                Debug.LogWarning("Нет свободных зон для спавна!");
                return;
            }

            GameObject enemyGO = null;

            if (_animalTypesHolder.MeleeAttackAnimals.Contains(enemyType))
            {
                enemyGO = _meleeFactory.Create(enemyType)?.gameObject;
            }
            else if (_animalTypesHolder.RangeAttackAnimals.Contains(enemyType))
            {
                enemyGO = _rangeFactory.Create(enemyType)?.gameObject;
            }

            if (enemyGO != null)
            {
                var sceneEntity = enemyGO.GetComponent<SceneEntity>();
                ConfigureEnemy(zone.SpawnPoint.position, sceneEntity, entryStats);
                OnEnemySpawned?.Invoke(sceneEntity);
            }
        }

        // ? Выбираем случайную свободную зону
        private AnimalSpawnZone GetRandomFreeZone()
        {
            var freeZones = _zones.FindAll(z => !z.IsOccupied);
            if (freeZones.Count == 0) return null;

            return freeZones[Random.Range(0, freeZones.Count)];
        }

        private void ConfigureEnemy(Vector3 position, SceneEntity sceneEntity, EnemyStats entryStats)
        {
            var controller = sceneEntity.GetCharacterController();

            sceneEntity.GetHitPoints().Value = entryStats.CurrentHealth;
            sceneEntity.GetMoveSpeed().Value = entryStats.CurrentSpeed;
            sceneEntity.GetAttackDamage().Value = entryStats.CurrentAttackPower;

            controller.enabled = false;

            sceneEntity.transform.position = position;


            sceneEntity.GetGlobalTarget().Value = _basePosition;
            var offset = sceneEntity.GetRootTransform().position;

            sceneEntity.GetRootTransform().position = new Vector3(position.x, offset.y, position.z);
            controller.enabled = true;
        }
    }
}
