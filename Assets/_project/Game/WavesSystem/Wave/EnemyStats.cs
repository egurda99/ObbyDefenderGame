using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class EnemyStats
    {
        private AnimalType _enemyId;

        public AnimalType EnemyId => _enemyId;


        [Header("Базовые характеристики")] [SerializeField]
        private float _baseHealth;

        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _baseAttackPower;

        [Header("Рост параметров по волнам (множители)")] [SerializeField]
        private float _healthGrowthPerWave; // +5% за волну

        [SerializeField] private float _speedGrowthPerWave; // +1% за волну
        [SerializeField] private float _attackGrowthPerWave; // +3% за волну

        [Header("Сложность врага (вес для баланса)")] [SerializeField]
        private float _difficultyWeight; // насколько "дорогой" враг

        public float BaseHealth => _baseHealth;

        public float BaseSpeed => _baseSpeed;

        public float BaseAttackPower => _baseAttackPower;

        public float HealthGrowthPerWave => _healthGrowthPerWave;

        public float SpeedGrowthPerWave => _speedGrowthPerWave;

        public float AttackGrowthPerWave => _attackGrowthPerWave;

        public float DifficultyWeight => _difficultyWeight;


        public float CurrentHealth => _currentHealth;

        public float CurrentSpeed => _currentSpeed;

        public float CurrentAttackPower => _currentAttackPower;

        [Header("Реальные характеристики")] [ShowInInspector] [ReadOnly]
        private float _currentHealth;

        [ShowInInspector] [ReadOnly] private float _currentSpeed;
        [ShowInInspector] [ReadOnly] private float _currentAttackPower;

        public EnemyStats(AnimalType enemyId, float difficultyWeight, float currentHealth, float currentSpeed,
            float currentAttackPower)
        {
            _enemyId = enemyId;
            _difficultyWeight = difficultyWeight;
            _currentHealth = currentHealth;
            _currentSpeed = currentSpeed;
            _currentAttackPower = currentAttackPower;
        }


        public void SetAnimalType(AnimalType animalType)
        {
            _enemyId = animalType;
        }

        public void SetHealth(float health)
        {
            _currentHealth = health;
        }

        public void SetSpeed(float speed)
        {
            _currentSpeed = speed;
        }

        public void SetAttackPower(float power)
        {
            _currentAttackPower = power;
        }
    }
}
