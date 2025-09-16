using _UpgradePractice.Scripts;
using UnityEngine;

namespace ObbyDefender.TurretUpgrades
{
    [CreateAssetMenu(
        fileName = "TurretHealthUpgradeConfig",
        menuName = "Configs/Upgrade/New TurretHealthUpgradeConfig"
    )]
    public class TurretHealthUpgradeConfig : UpgradeConfig
    {
        public TurretHealthTable _turretHealthTable;

        public override Upgrade Create()
        {
            return new TurretHealthUpgrade(this);
        }

        public override float GetStatValue(int level)
        {
            if (level >= 1 && level <= MaxLevel)
            {
                return _turretHealthTable.GetHealth(level);
            }

            return _turretHealthTable.GetHealth(MaxLevel);
        }


        protected override void OnValidate()
        {
            base.OnValidate();
            _turretHealthTable.OnValidate(MaxLevel);
        }
    }
}
