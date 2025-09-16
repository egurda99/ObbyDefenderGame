using System;
using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class EnemiesRemainingHandler : IDisposable
    {
        private readonly ActiveEnemiesProvider _activeEnemiesProvider;
        private readonly WavesSystem _wavesSystem;

        private int _enemiesRemaining;

        public event Action<int> OnEnemiesChanged;
        public event Action<int> WaveEnded;

        public int EnemiesRemaining => _enemiesRemaining;

        public EnemiesRemainingHandler(ActiveEnemiesProvider activeEnemiesProvider, WavesSystem wavesSystem)
        {
            _activeEnemiesProvider = activeEnemiesProvider;
            _wavesSystem = wavesSystem;
            _activeEnemiesProvider.OnActiveEnemyDead += OnEnemyDead;

            _enemiesRemaining = _wavesSystem.GetCurrentWave().Enemies.Count;
            Debug.Log($"<color=blue>Enemies in wave : {_enemiesRemaining}</color>");
        }

        private void OnEnemyDead(SceneEntity sceneEntity)
        {
            _enemiesRemaining--;
            OnEnemiesChanged?.Invoke(_enemiesRemaining);
            if (_enemiesRemaining <= 0)
            {
                WaveEnded?.Invoke(_wavesSystem.GetCurrentWave().Reward);
            }
        }

        public int GetKilledEnemies()
        {
            var killedEnemies = _wavesSystem.GetCurrentWave().Enemies.Count - _enemiesRemaining;
            return killedEnemies;
        }


        public void Dispose()
        {
            _activeEnemiesProvider.OnActiveEnemyDead -= OnEnemyDead;
        }
    }
}
