using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public class MeleeEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly List<MeleeBrainrotAnimalInstaller> _prefabs;

        [Inject]
        public MeleeEnemyFactory(DiContainer container, List<MeleeBrainrotAnimalInstaller> prefabs)
        {
            _container = container;
            _prefabs = prefabs;
        }

        public IEnemy Create(EnemyType type)
        {
            // Находим префаб для данного типа
            var prefab = _prefabs.FirstOrDefault(p => p.EnemyType == type);
            if (prefab == null)
            {
                Debug.LogWarning($"Нет префаба для типа {type}");
                return null;
            }

            // Резолвим пул через контейнер с ID = EnemyType
            var pool = _container.ResolveId<MeleeEnemyPool>(type);
            return pool.Spawn();
        }

        public IEnemy CreateRandom()
        {
            var prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            var pool = _container.ResolveId<MeleeEnemyPool>(prefab.EnemyType);
            return pool.Spawn();
        }
    }
}
