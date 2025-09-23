using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class EnemyRealStats
    {
        [SerializeField] [ReadOnly] private float _currentHealth;
        [SerializeField] [ReadOnly] private float _currentSpeed;
        [SerializeField] [ReadOnly] private float _currentAttackPower;
        [SerializeField] private float _difficultyWeight;

        private AnimalType _enemyId;
        public AnimalType EnemyId => _enemyId;
        public float CurrentHealth => _currentHealth;
        public float CurrentSpeed => _currentSpeed;
        public float CurrentAttackPower => _currentAttackPower;
        public float DifficultyWeight => _difficultyWeight;

        public EnemyRealStats(AnimalType id, float weight, float health, float speed, float attack)
        {
            _enemyId = id;
            _difficultyWeight = weight;
            _currentHealth = health;
            _currentSpeed = speed;
            _currentAttackPower = attack;
        }
    }
}
