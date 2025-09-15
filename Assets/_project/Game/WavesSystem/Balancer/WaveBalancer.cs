using System.Collections.Generic;
using UnityEngine;

namespace ObbyDefender
{
    public class WaveBalancer
    {
        private readonly EnemyConfigDatabase _enemyDatabase;
        private readonly BalanceFormulaConfig _formula;

        public WaveBalancer(EnemyConfigDatabase enemyDatabase, BalanceFormulaConfig formula)
        {
            _enemyDatabase = enemyDatabase;
            _formula = formula;
        }

        public List<Wave> GenerateWaves(int waveCount)
        {
            var waves = new List<Wave>();

            for (var i = 1; i <= waveCount; i++)
            {
                var wave = GenerateWave(i);
                waves.Add(wave);
            }

            return waves;
        }

        private Wave GenerateWave(int waveIndex)
        {
            var wave = new Wave { WaveIndex = waveIndex };

            // 1. Считаем сколько врагов в этой волне
            var enemyCount = Mathf.RoundToInt(
                Mathf.Lerp(_formula.MinEnemiesPerWave, _formula.MaxEnemiesPerWave, waveIndex / 20f)
            );

            // 2. Распределяем ближники/дальники (например 70/30)
            var meleeCount = Mathf.RoundToInt(enemyCount * 0.7f);
            var rangeCount = enemyCount - meleeCount;

            // 3. Выбираем врагов случайно из базы
            var meleeEnemies = _enemyDatabase.Enemies.FindAll(e => e.EnemyType == AnimalAttackType.Melee);
            var rangeEnemies = _enemyDatabase.Enemies.FindAll(e => e.EnemyType == AnimalAttackType.Range);

            // Добавляем ближников
            AddEnemiesToWave(wave, meleeEnemies, meleeCount, waveIndex);
            // Добавляем дальников
            AddEnemiesToWave(wave, rangeEnemies, rangeCount, waveIndex);

            // 4. Подсчёт сложности
            wave.TotalDifficulty = CalculateWaveDifficulty(wave);
            wave.Reward = Mathf.RoundToInt(wave.TotalDifficulty * _formula.RewardFactor);

            return wave;
        }

        private void AddEnemiesToWave(Wave wave, List<EnemyConfigDatabase.EnemyConfig> candidates, int count,
            int waveIndex)
        {
            if (candidates.Count == 0 || count <= 0)
                return;

            for (var i = 0; i < count; i++)
            {
                var config = candidates[Random.Range(0, candidates.Count)];

                var scaledStats = ScaleStats(config.Stats, waveIndex);

                wave.Enemies.Add(new EnemyEntry
                {
                    EnemyId = config.EnemyId,
                    Count = 1, // один враг
                    Stats = scaledStats
                });
            }
        }

        private EnemyStats ScaleStats(EnemyStats baseStats, int waveIndex)
        {
            // return new EnemyStats
            // {
            //     //EnemyId = baseStats.EnemyId,
            //     EnemyId = baseStats.SetAnimalType(baseStats.EnemyId),
            //     BaseHealth = baseStats.BaseHealth * Mathf.Pow(baseStats.HealthGrowthPerWave, waveIndex),
            //     BaseSpeed = baseStats.BaseSpeed * Mathf.Pow(baseStats.SpeedGrowthPerWave, waveIndex),
            //     BaseAttackPower = baseStats.BaseAttackPower * Mathf.Pow(baseStats.AttackGrowthPerWave, waveIndex),
            //     DifficultyWeight = baseStats.DifficultyWeight
            // };

            return new EnemyStats(baseStats.EnemyId,
                baseStats.DifficultyWeight,
                baseStats.BaseHealth * Mathf.Pow(baseStats.HealthGrowthPerWave, waveIndex),
                baseStats.BaseSpeed * Mathf.Pow(baseStats.SpeedGrowthPerWave, waveIndex),
                baseStats.BaseAttackPower * Mathf.Pow(baseStats.AttackGrowthPerWave, waveIndex));
        }

        private float CalculateWaveDifficulty(Wave wave)
        {
            var difficulty = 0f;

            foreach (var enemy in wave.Enemies)
            {
                difficulty += CalculateEnemyDifficulty(enemy.Stats) * enemy.Count;
            }

            return difficulty;
        }

        private float CalculateEnemyDifficulty(EnemyStats stats)
        {
            return
                stats.BaseHealth * _formula.HealthWeight +
                stats.BaseAttackPower * _formula.AttackWeight +
                stats.BaseSpeed * _formula.SpeedWeight
                                * stats.DifficultyWeight;
        }
    }
}
