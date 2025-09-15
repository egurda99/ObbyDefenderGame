using System;
using System.Collections.Generic;
using Zenject;
using Random = UnityEngine.Random;

namespace ObbyDefender
{
    public sealed class MeleeAnimalFactory
    {
        private readonly DiContainer _container;
        private readonly List<MeleeBrainrotAnimalInstaller> _prefabs;
        private readonly EnemyConfigDatabase _database;

        // public MeleeAnimalFactory(DiContainer container, List<MeleeBrainrotAnimalInstaller> prefabs)
        // {
        //     _container = container;
        //     _prefabs = prefabs;
        // }
        public MeleeAnimalFactory(DiContainer container, EnemyConfigDatabase database)
        {
            _database = database;
            _container = container;
        }

        public MeleeBrainrotAnimalInstaller Create(AnimalType type)
        {
            // MeleeBrainrotAnimalInstaller prefab = null;
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

            var pool = _container.ResolveId<MeleeAnimalPool>(type);
            return pool.Spawn();
        }

        public MeleeBrainrotAnimalInstaller CreateRandom()
        {
            var prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            var pool = _container.ResolveId<MeleeAnimalPool>(prefab.AnimalType);
            return pool.Spawn();
        }
    }
}
