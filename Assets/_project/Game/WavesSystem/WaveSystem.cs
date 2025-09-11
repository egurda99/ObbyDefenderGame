using System.Collections.Generic;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class WaveSystem
    {
        private readonly List<Wave> _waves;

        public int CurrentWaveIndex { get; private set; }

        public WaveSystem(List<Wave> waves)
        {
            _waves = waves;
            CurrentWaveIndex = 1;
        }

        // ? Получить текущую волну
        public Wave GetCurrentWave()
        {
            if (_waves == null || _waves.Count == 0)
            {
                Debug.LogError("0 waves");

                return null;
            }

            if (CurrentWaveIndex >= _waves.Count)
            {
                Debug.LogError("invalid wave index");
                return null;
            }

            return _waves[CurrentWaveIndex];
        }

        // ? Переход к следующей волне
        public void MoveToNextWave()
        {
            if (CurrentWaveIndex < _waves.Count - 1)
                CurrentWaveIndex++;
        }

        // ? Проверка, есть ли следующая волна
        public bool HasNextWave()
        {
            return CurrentWaveIndex < _waves.Count - 1;
        }
    }
}
