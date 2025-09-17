using System.Collections.Generic;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class WavesSystem
    {
        private readonly List<Wave> _waves;

        public int CurrentWaveIndex { get; private set; }

        public WavesSystem(List<Wave> waves)
        {
            _waves = waves;
        }

        public void SetWave(int index)
        {
            CurrentWaveIndex = index;
        }

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

        public void MoveToNextWave()
        {
            if (HasNextWave())
                CurrentWaveIndex++;
        }

        public bool HasNextWave()
        {
            return CurrentWaveIndex < _waves.Count - 1;
        }
    }
}
