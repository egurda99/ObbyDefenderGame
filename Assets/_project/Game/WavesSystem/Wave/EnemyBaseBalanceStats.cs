using System;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class EnemyBaseBalanceStats
    {
        [Header("Базовые характеристики")] [SerializeField]
        private float _baseHealth;

        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _baseAttackPower;

        [Header("Рост параметров по волнам (множители)")] [SerializeField]
        private float _healthGrowthPerWave;

        [SerializeField] private float _speedGrowthPerWave;
        [SerializeField] private float _attackGrowthPerWave;

        [Header("Сложность врага (вес)")] [SerializeField]
        private float _difficultyWeight;

        private AnimalType _enemyId;

        public AnimalType EnemyId => _enemyId;
        public float BaseHealth => _baseHealth;
        public float BaseSpeed => _baseSpeed;
        public float BaseAttackPower => _baseAttackPower;
        public float HealthGrowthPerWave => _healthGrowthPerWave;
        public float SpeedGrowthPerWave => _speedGrowthPerWave;
        public float AttackGrowthPerWave => _attackGrowthPerWave;

        public float DifficultyWeight => _difficultyWeight;
    }
}
