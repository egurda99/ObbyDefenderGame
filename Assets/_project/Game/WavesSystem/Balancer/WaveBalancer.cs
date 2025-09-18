using System.Collections.Generic;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class WaveBalancer
    {
        private readonly EnemyConfigDatabase _enemyDatabase;
        private readonly WaveModuleBalancerConfig _formula;

        public WaveBalancer(EnemyConfigDatabase enemyDatabase, WaveModuleBalancerConfig formula)
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
            var enemies = new List<EnemyEntry>();

            // 1. Считаем сколько врагов в этой волне
            var enemyCount = Mathf.RoundToInt(
                Mathf.Lerp(_formula.MinEnemiesPerWave, _formula.MaxEnemiesPerWave, waveIndex / 20f)
            );

            // 2. Распределяем ближники/дальники (например 70/30)
            var meleeCount = Mathf.RoundToInt(enemyCount * 0.7f);
            var rangeCount = enemyCount - meleeCount;

            // 3. Выбираем врагов случайно из базы
            var meleeEnemies = _enemyDatabase.Enemies
                .FindAll(e => e.EnemyType == AnimalAttackType.Melee);
            var rangeEnemies = _enemyDatabase.Enemies
                .FindAll(e => e.EnemyType == AnimalAttackType.Range);

            // Добавляем ближников
            AddEnemiesToWave(enemies, meleeEnemies, meleeCount, waveIndex);
            // Добавляем дальников
            AddEnemiesToWave(enemies, rangeEnemies, rangeCount, waveIndex);

            // 4. Подсчёт сложности
            var totalDifficulty = CalculateWaveDifficulty(enemies);
            var reward = Mathf.RoundToInt(totalDifficulty * _formula.RewardFactor);

            // 5. Возвращаем собранную волну
            return new Wave(waveIndex, totalDifficulty, reward, enemies);
        }

        private void AddEnemiesToWave(List<EnemyEntry> targetList, List<EnemyConfigDatabase.EnemyConfig> candidates,
            int count,
            int waveIndex)
        {
            for (var i = 0; i < count; i++)
            {
                var enemy = candidates[Random.Range(0, candidates.Count)];
                var scaledStats = ScaleStats(enemy.BaseBalanceStats, waveIndex);
                targetList.Add(new EnemyEntry(enemy.EnemyId, 1, scaledStats));
            }
        }

        private EnemyRealStats ScaleStats(EnemyBaseBalanceStats baseBaseBalanceStats, int waveIndex)
        {
            var scaled = new EnemyRealStats(
                baseBaseBalanceStats.EnemyId,
                baseBaseBalanceStats.DifficultyWeight,
                baseBaseBalanceStats.BaseHealth * Mathf.Pow(baseBaseBalanceStats.HealthGrowthPerWave, waveIndex),
                baseBaseBalanceStats.BaseSpeed * Mathf.Pow(baseBaseBalanceStats.SpeedGrowthPerWave, waveIndex),
                baseBaseBalanceStats.BaseAttackPower * Mathf.Pow(baseBaseBalanceStats.AttackGrowthPerWave, waveIndex)
            );
            return scaled;
        }

        private float CalculateWaveDifficulty(List<EnemyEntry> wave)
        {
            var difficulty = 0f;

            foreach (var enemy in wave)
            {
                difficulty += CalculateEnemyDifficulty(enemy.Stats) * enemy.Count;
            }

            return difficulty;
        }

        private float CalculateEnemyDifficulty(EnemyRealStats realStats)
        {
            return
                realStats.CurrentHealth * _formula.HealthWeight +
                realStats.CurrentAttackPower * _formula.AttackWeight +
                realStats.CurrentSpeed * _formula.SpeedWeight
                                       * realStats.DifficultyWeight;
        }
    }
}
