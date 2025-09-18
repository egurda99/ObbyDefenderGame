using System;
using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class ActiveEnemiesProvider : IDisposable
    {
        private readonly AnimalSpawner _animalSpawner;

        private readonly List<EnemySubscription> _activeEnemies = new();

        public event Action<SceneEntity> ActiveEnemyDead;

        public ActiveEnemiesProvider(AnimalSpawner animalSpawner)
        {
            _animalSpawner = animalSpawner;
            _animalSpawner.OnEnemySpawned += OnEnemySpawned;
        }

        private void OnEnemySpawned(SceneEntity enemy)
        {
            if (enemy == null)
                return;

            Action<bool> handler = null;
            handler = isDead => OnEnemyIsDeadChanged(enemy, isDead);

            enemy.GetIsDead().Subscribe(handler);

            _activeEnemies.Add(new EnemySubscription(enemy, handler));
        }

        private void OnEnemyIsDeadChanged(SceneEntity enemy, bool isDead)
        {
            if (!isDead)
                return;

            var index = _activeEnemies.FindIndex(e => e.Enemy == enemy);
            if (index < 0)
                return;

            ActiveEnemyDead?.Invoke(_activeEnemies[index].Enemy);

            _activeEnemies[index].Unsubscribe();
            _activeEnemies.RemoveAt(index);
        }

        public void Dispose()
        {
            _animalSpawner.OnEnemySpawned -= OnEnemySpawned;

            foreach (var sub in _activeEnemies)
                sub.Unsubscribe();

            _activeEnemies.Clear();
        }

        public IReadOnlyList<SceneEntity> GetActiveEnemies()
        {
            var result = new List<SceneEntity>(_activeEnemies.Count);
            for (var index = 0; index < _activeEnemies.Count; index++)
            {
                var sub = _activeEnemies[index];
                result.Add(sub.Enemy);
            }

            return result.AsReadOnly();
        }

        private readonly struct EnemySubscription
        {
            public SceneEntity Enemy { get; }
            private readonly Action<bool> _handler;

            public EnemySubscription(SceneEntity enemy, Action<bool> handler)
            {
                Enemy = enemy;
                _handler = handler;
            }

            public void Unsubscribe()
            {
                if (Enemy != null && _handler != null)
                {
                    try
                    {
                        Enemy.GetIsDead().Unsubscribe(_handler);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"Unsubscribe failed for {Enemy.name}: {ex}");
                    }
                }
            }
        }
    }
}
