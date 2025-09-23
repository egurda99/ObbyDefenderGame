using System;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class EnemyEntry
    {
        [SerializeField] private AnimalType _enemyId;
        [SerializeField] private int _count;
        [SerializeField] private EnemyRealStats _realStats;

        public AnimalType EnemyId => _enemyId;
        public int Count => _count;
        public EnemyRealStats Stats => _realStats;

        public EnemyEntry(AnimalType enemyId, int count, EnemyRealStats stats)
        {
            _enemyId = enemyId;
            _count = count;
            _realStats = stats;
        }
    }
}
