using System;
using System.Collections.Generic;
using Zenject;
using Random = UnityEngine.Random;

namespace ObbyDefender
{
    public sealed class RangeAnimalFactory
    {
        private readonly DiContainer _container;
        private readonly List<RangeBrainrotAnimalInstaller> _prefabs;
        private readonly EnemyConfigDatabase _database;

        // public RangeAnimalFactory(DiContainer container, List<RangeBrainrotAnimalInstaller> prefabs)
        // {
        //     _container = container;
        //     _prefabs = prefabs;
        // }

        public RangeAnimalFactory(DiContainer container, EnemyConfigDatabase database)
        {
            _database = database;
            _container = container;
        }

        public RangeBrainrotAnimalInstaller Create(AnimalType type)
        {
            // RangeBrainrotAnimalInstaller prefab = null;
            // foreach (var p in _prefabs)
            // {
            //     if (p.AnimalType == type)
            //     {
            //         prefab = p;
            //         break;
            //     }
            // }
            //
            // if (prefab == null)
            // {
            //     Debug.LogWarning($"Нет префаба для типа {type}");
            //     return null;
            // }

            var prefab = _database.GetPrefab(type);
            if (prefab == null)
                throw new ArgumentException($"Prefab not found for AnimalType {type}");

            var pool = _container.ResolveId<RangeAnimalPool>(type);
            return pool.Spawn();
        }

        public RangeBrainrotAnimalInstaller CreateRandom()
        {
            var prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            var pool = _container.ResolveId<RangeAnimalPool>(prefab.AnimalType);
            return pool.Spawn();
        }
    }
}
