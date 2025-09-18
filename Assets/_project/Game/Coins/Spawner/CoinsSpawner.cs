using System;
using Atomic.Entities;
using MyCodeBase;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObbyDefender
{
    public sealed class CoinsSpawner : IDisposable
    {
        private readonly ActiveEnemiesProvider _activeEnemiesProvider;
        private readonly WavesSystem _wavesSystem;
        private readonly CoinsPool _coinsPool;
        private readonly CoinsBalanceConfig _coinsBalanceConfig;
        private readonly MoneyStorage _moneyStorage;

        public CoinsSpawner(WavesSystem wavesSystem, CoinsBalanceConfig coinsBalanceConfig,
            ActiveEnemiesProvider activeEnemiesProvider, CoinsPool coinsPool, MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _coinsBalanceConfig = coinsBalanceConfig;
            _coinsPool = coinsPool;
            _wavesSystem = wavesSystem;

            _activeEnemiesProvider = activeEnemiesProvider;

            _activeEnemiesProvider.ActiveEnemyDead += EnemyDied;
        }

        private void EnemyDied(SceneEntity sceneEntity)
        {
            var chance = CalculateDropChance(_wavesSystem.GetCurrentWave().TotalDifficulty);
            if (Random.value <= chance)
            {
                SpawnCoin(sceneEntity.GetRootTransform().position);
            }
        }

        private float CalculateDropChance(float difficulty)
        {
            var t = Mathf.Clamp01(difficulty / 100f);
            return Mathf.Lerp(_coinsBalanceConfig.MinChanceDrop, _coinsBalanceConfig.MaxChanceDrop, t);
        }

        private int CalculateMoneyDrop(float difficulty)
        {
            var t = Mathf.Clamp01(difficulty / 1000f);


            var coins = Mathf.Lerp(_coinsBalanceConfig.MinValueCoin, _coinsBalanceConfig.MaxValueCoin, t);

            return Mathf.RoundToInt(coins);
        }

        private void SpawnCoin(Vector3 position)
        {
            var coin = _coinsPool.Spawn();
            var coinEntity = coin.GetComponent<SceneEntity>();

            coinEntity.GetBehaviour<AddToMoneyStorageBehaviour>().SetMoneyStorage(_moneyStorage);
            coinEntity.GetRootTransform().position = position;
            coinEntity.GetPointsValue().Value = CalculateMoneyDrop(_wavesSystem.GetCurrentWave().TotalDifficulty);
        }

        public void Dispose()
        {
            _activeEnemiesProvider.ActiveEnemyDead -= EnemyDied;
        }
    }
}