using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class Wave
    {
        [SerializeField] private int _waveIndex;
        [SerializeField] private List<EnemyEntry> _enemies = new();
        [SerializeField] private float _totalDifficulty;
        [SerializeField] private int _reward;

        public int WaveIndex => _waveIndex;
        public IReadOnlyList<EnemyEntry> Enemies => _enemies;
        public float TotalDifficulty => _totalDifficulty;
        public int Reward => _reward;

        public Wave(int waveIndex, float totalDifficulty, int reward, List<EnemyEntry> enemies)
        {
            _waveIndex = waveIndex;
            _totalDifficulty = totalDifficulty;
            _reward = reward;
            _enemies = enemies ?? new List<EnemyEntry>();
        }
    }
}
