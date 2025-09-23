using _UpgradePractice.Scripts;
using UnityEngine;

namespace ObbyDefender.Scripts
{
    [CreateAssetMenu(
        fileName = "HealthUpgradeConfig",
        menuName = "Configs/Upgrade/New HealthUpgradeConfig"
    )]
    public class HealthUpgradeConfig : UpgradeConfig
    {
        public HealthTable HealthTable;

        public override Upgrade Create()
        {
            return new HealthUpgrade(this);
        }

        public override float GetStatValue(int level)
        {
            if (level >= 1 && level <= MaxLevel)
            {
                return HealthTable.GetHealth(level);
            }

            return HealthTable.GetHealth(MaxLevel);
        }


        protected override void OnValidate()
        {
            base.OnValidate();
            HealthTable.OnValidate(MaxLevel);
        }
    }
}
