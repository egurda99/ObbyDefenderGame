using _UpgradePractice.Scripts;
using UnityEngine;

namespace ObbyDefender.DI
{
    public sealed class GameBootstrapHelper : MonoBehaviour
    {
        [SerializeField] private UpgradeCatalog _upgradeCatalog;

        [Space] [Header("Wave module settings")] [SerializeField]
        private WaveModuleBalancerConfig _formulaConfig;

        [SerializeField] private EnemyConfigDatabase _enemyConfigDatabase;
        public EnemyConfigDatabase EnemyConfigDatabase => _enemyConfigDatabase;

        public UpgradeCatalog UpgradeCatalog => _upgradeCatalog;

        public WaveModuleBalancerConfig FormulaConfig => _formulaConfig;
    }
}
