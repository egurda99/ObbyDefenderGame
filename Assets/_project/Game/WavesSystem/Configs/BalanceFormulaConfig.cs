using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(fileName = "BalanceFormulaConfig", menuName = "Game/Configs/Balance Formula Config")]
    public sealed class BalanceFormulaConfig : ScriptableObject
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

        public float HealthWeight => _healthWeight;

        public float AttackWeight => _attackWeight;

        public float SpeedWeight => _speedWeight;

        public float RewardFactor => _rewardFactor;

        public int MinEnemiesPerWave => _minEnemiesPerWave;

        public int MaxEnemiesPerWave => _maxEnemiesPerWave;
    }
}
