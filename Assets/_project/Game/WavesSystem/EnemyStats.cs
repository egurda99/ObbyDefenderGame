using System;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class EnemyStats
    {
        [HideInInspector] public AnimalType EnemyId;

        [Header("Ѕазовые характеристики")] public float BaseHealth = 100f;
        public float BaseSpeed = 3f;
        public float BaseAttackPower = 10f;

        [Header("–ост параметров по волнам (множители)")]
        public float HealthGrowthPerWave = 1.05f; // +5% за волну

        public float SpeedGrowthPerWave = 1.01f; // +1% за волну
        public float AttackGrowthPerWave = 1.03f; // +3% за волну

        [Header("—ложность врага (вес дл€ баланса)")]
        public float DifficultyWeight = 1f; // насколько "дорогой" враг
    }
}
