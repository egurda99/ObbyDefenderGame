using _UpgradePractice.Scripts;
using UnityEngine;

namespace ObbyDefender.Scripts
{
    [CreateAssetMenu(
        fileName = "TurretDamageUpgradeConfig",
        menuName = "Configs/Upgrade/New TurretDamageUpgradeConfig"
    )]
    public class TurretDamageUpgradeConfig : UpgradeConfig
    {
        public DamageUpgradeTable DamageUpgradeTable;

        public override Upgrade Create()
        {
            return new TurretDamageUpgrade(this);
        }

        public override float GetStatValue(int level)
        {
            if (level >= 1 && level <= MaxLevel)
            {
                return DamageUpgradeTable.GetDamage(level);
            }

            return DamageUpgradeTable.GetDamage(MaxLevel);
        }


        protected override void OnValidate()
        {
            base.OnValidate();
            DamageUpgradeTable.OnValidate(MaxLevel);
        }
    }
}
