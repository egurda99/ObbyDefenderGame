using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(
        fileName = "MoneyBalanceConfig",
        menuName = "SO/New MoneyBalanceConfig"
    )]
    public sealed class CoinsBalanceConfig : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] private float _minChanceDrop = 0.1f;
        [SerializeField] [Range(0, 1)] private float _maxChanceDrop = 0.25f;
        [SerializeField] [Range(5, 100)] private int _minValueCoin = 5;
        [SerializeField] [Range(5, 100)] private int _maxValueCoin = 15;

        public float MinChanceDrop => _minChanceDrop;

        public float MaxChanceDrop => _maxChanceDrop;

        public int MinValueCoin => _minValueCoin;

        public int MaxValueCoin => _maxValueCoin;
    }
}
