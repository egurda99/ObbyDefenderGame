using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(fileName = "WaveModuleBalancerConfig", menuName = "Game/Configs/WaveModule Balancer Config")]
    public sealed class WaveModuleBalancerConfig : ScriptableObject
    {
        [Header("Вес параметров врага")] [SerializeField]
        private float _healthWeight = 0.5f;

        [SerializeField] private float _attackWeight = 1.0f;
        [SerializeField] private float _speedWeight = 0.2f;

        [Header("Баланс наград")] [SerializeField]
        private float _rewardFactor = 0.1f;

        [Header("Границы сложности волн")] [SerializeField]
        private int _minEnemiesPerWave = 20;

        [SerializeField] private int _maxEnemiesPerWave = 100;

        [SerializeField] private float _minSpawnDelay = 1;
        [SerializeField] private float _maxSpawnDelay = 3;
        [SerializeField] private int _waveCount = 15;

        public int WaveCount => _waveCount;

        public float MinSpawnDelay => _minSpawnDelay;

        public float MaxSpawnDelay => _maxSpawnDelay;

        public float HealthWeight => _healthWeight;

        public float AttackWeight => _attackWeight;

        public float SpeedWeight => _speedWeight;

        public float RewardFactor => _rewardFactor;

        public int MinEnemiesPerWave => _minEnemiesPerWave;

        public int MaxEnemiesPerWave => _maxEnemiesPerWave;
    }
}
