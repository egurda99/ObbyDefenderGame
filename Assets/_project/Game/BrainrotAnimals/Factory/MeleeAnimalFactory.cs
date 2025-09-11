using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class MeleeAnimalFactory
    {
        private readonly DiContainer _container;
        private readonly List<MeleeBrainrotAnimalInstaller> _prefabs;

        public MeleeAnimalFactory(DiContainer container, List<MeleeBrainrotAnimalInstaller> prefabs)
        {
            _container = container;
            _prefabs = prefabs;
        }

        public MeleeBrainrotAnimalInstaller Create(AnimalType type)
        {
            MeleeBrainrotAnimalInstaller prefab = null;
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
