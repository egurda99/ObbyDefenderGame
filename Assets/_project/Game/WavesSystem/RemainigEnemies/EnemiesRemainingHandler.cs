using System;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class EnemiesRemainingHandler : IDisposable
    {
        private readonly ActiveEnemiesProvider _activeEnemiesProvider;
        private readonly WavesSystem _wavesSystem;

        private int _enemiesRemaining;

        public event Action<int> OnEnemiesChanged;

        public int EnemiesRemaining => _enemiesRemaining;

        public EnemiesRemainingHandler(ActiveEnemiesProvider activeEnemiesProvider, WavesSystem wavesSystem)
        {
            _activeEnemiesProvider = activeEnemiesProvider;
            _wavesSystem = wavesSystem;
            _activeEnemiesProvider.OnActiveEnemyDead += OnEnemyDead;

            _enemiesRemaining = _wavesSystem.GetCurrentWave().Enemies.Count;
            Debug.Log($"<color=blue>Enemies in wave : {_enemiesRemaining}</color>");
        }

        private void OnEnemyDead()
        {
            _enemiesRemaining--;
            OnEnemiesChanged?.Invoke(_enemiesRemaining);
            if (_enemiesRemaining <= 0)
            {
                Debug.Log("<color=red>Enemies ended. Game ENDED</color>");
                Debug.Log($"<color=orange>reward is {_wavesSystem.GetCurrentWave().Reward}</color>");
            }
        }

        public void Dispose()
        {
            _activeEnemiesProvider.OnActiveEnemyDead -= OnEnemyDead;
        }
    }
}
