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
        private readonly WaveSystem _waveSystem;

        public AnimalSpawner(
            List<AnimalSpawnZone> zones,
            Transform basePosition,
            MeleeAnimalFactory meleeFactory,
            RangeAnimalFactory rangeFactory,
            AnimalTypesHolder animalTypesHolder,
            WaveSystem waveSystem,
            float minSpawnDelay,
            float maxSpawnDelay)
        {
            _waveSystem = waveSystem;
            _animalTypesHolder = animalTypesHolder;
            _zones = zones;
            _basePosition = basePosition;
            _meleeFactory = meleeFactory;
            _rangeFactory = rangeFactory;
            _minSpawnDelay = minSpawnDelay;
            _maxSpawnDelay = maxSpawnDelay;
        }

        // ? Асинхронный спавн волны
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
                    SpawnEnemy(entry.EnemyId);

                    var delay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
                    await UniTask.Delay(TimeSpan.FromSeconds(delay));
                }
            }
        }

        // ? Спавн конкретного врага из волны
        private void SpawnEnemy(AnimalType enemyType)
        {
            var zone = GetRandomFreeZone();
            if (zone == null)
            {
                Debug.LogWarning("Нет свободных зон для спавна!");
                return;
            }

            GameObject enemyGO = null;

            // Определяем фабрику по типу врага
            if (_animalTypesHolder.MeleeAttackAnimals.Contains(enemyType))
            {
                enemyGO = _meleeFactory.Create(enemyType)?.gameObject;
            }
            else if (_animalTypesHolder.RangeAttackAnimals.Contains(enemyType))
            {
                enemyGO = _rangeFactory.Create(enemyType)?.gameObject;
            }

            if (enemyGO != null)
                ConfigureEnemy(zone.SpawnPoint.position, enemyGO);
        }

        // ? Выбираем случайную свободную зону
        private AnimalSpawnZone GetRandomFreeZone()
        {
            var freeZones = _zones.FindAll(z => !z.IsOccupied);
            if (freeZones.Count == 0) return null;

            return freeZones[Random.Range(0, freeZones.Count)];
        }

        private void ConfigureEnemy(Vector3 position, GameObject enemyGO)
        {
            var sceneEntity = enemyGO.GetComponent<SceneEntity>();
            var controller = sceneEntity.GetCharacterController();
            controller.enabled = false;

            enemyGO.transform.position = position;


            sceneEntity.GetGlobalTarget().Value = _basePosition;
            var offset = sceneEntity.GetRootTransform().position;

            sceneEntity.GetRootTransform().position = new Vector3(position.x, offset.y, position.z);
            controller.enabled = true;
        }

        public void OnStartGame()
        {
            SpawnWaveAsync(_waveSystem.GetCurrentWave()).Forget();
        }
    }
}
