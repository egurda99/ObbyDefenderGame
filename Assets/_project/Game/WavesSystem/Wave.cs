using System;
using System.Collections.Generic;

namespace ObbyDefender
{
    [Serializable]
    public class Wave
    {
        public int WaveIndex;
        public List<EnemyEntry> Enemies = new();
        public float TotalDifficulty;
        public int Reward;
    }
}