using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public class RangeEnemyFactory
    {
        private readonly Dictionary<EnemyType, RangeEnemyPool> _pools;
        private readonly DiContainer _container;

        [Inject]
        public RangeEnemyFactory(Dictionary<EnemyType, RangeEnemyPool> pools, DiContainer container)
        {
            _pools = pools;
            _container = container;
        }

        public IEnemy Create(EnemyType type)
        {
            if (_pools.TryGetValue(type, out var pool))
                return pool.Spawn();

            Debug.LogWarning($"Нет пула для типа {type}");
            return null;
        }

        public IEnemy CreateRandom()
        {
            var types = new List<EnemyType>(_pools.Keys);
            return Create(types[Random.Range(0, types.Count)]);
        }
    }
}
