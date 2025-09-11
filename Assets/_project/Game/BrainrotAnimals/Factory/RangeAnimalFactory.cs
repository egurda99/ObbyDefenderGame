using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class RangeAnimalFactory
    {
        private readonly DiContainer _container;
        private readonly List<RangeBrainrotAnimalInstaller> _prefabs;

        public RangeAnimalFactory(DiContainer container, List<RangeBrainrotAnimalInstaller> prefabs)
        {
            _container = container;
            _prefabs = prefabs;
        }

        public RangeBrainrotAnimalInstaller Create(AnimalType type)
        {
            RangeBrainrotAnimalInstaller prefab = null;
            foreach (var p in _prefabs)
            {
                if (p.AnimalType == type)
                {
                    prefab = p;
                    break;
                }
            }

            if (prefab == null)
            {
                Debug.LogWarning($"Нет префаба для типа {type}");
                return null;
            }

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
