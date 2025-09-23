using _UpgradePractice.Scripts;
using UnityEngine;

namespace ObbyDefender.Scripts
{
    [CreateAssetMenu(
        fileName = "SpeedUpgradeConfig",
        menuName = "Configs/Upgrade/New SpeedUpgradeUpgradeConfig"
    )]
    public class SpeedUpgradeConfig : UpgradeConfig
    {
        public MoveSpeedTable MoveSpeedTable;

        public override Upgrade Create()
        {
            return new SpeedUpgrade(this);
        }

        public override float GetStatValue(int level)
        {
            if (level >= 1 && level <= MaxLevel)
            {
                return MoveSpeedTable.GetSpeed(level);
            }

            return MoveSpeedTable.GetSpeed(MaxLevel);
        }


        protected override void OnValidate()
        {
            base.OnValidate();
            MoveSpeedTable.OnValidate(MaxLevel);
        }
    }
}
