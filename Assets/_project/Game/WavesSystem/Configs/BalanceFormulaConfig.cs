using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(fileName = "BalanceFormulaConfig", menuName = "Game/Configs/Balance Formula Config")]
    public sealed class BalanceFormulaConfig : ScriptableObject
    {
        [Header("Вес параметров врага")] public float HealthWeight = 0.5f;
        public float AttackWeight = 1.0f;
        public float SpeedWeight = 0.2f;

        [Header("Баланс наград")] public float RewardFactor = 0.1f;

        [Header("Границы сложности волн")] public int MinEnemiesPerWave = 20;
        public int MaxEnemiesPerWave = 100;
    }
}
