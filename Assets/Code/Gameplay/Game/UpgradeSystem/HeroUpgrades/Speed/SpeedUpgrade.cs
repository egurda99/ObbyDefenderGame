using _UpgradePractice.Scripts;
using UnityEngine;
using Zenject;

namespace ObbyDefender.Scripts
{
    public sealed class SpeedUpgrade : Upgrade
    {
        private readonly SpeedUpgradeConfig _speedUpgradeConfig;
        private HeroData _heroData;

        public SpeedUpgrade(SpeedUpgradeConfig config) : base(config)
        {
            _speedUpgradeConfig = config;
        }

        [Inject]
        public void Construct(HeroData heroData)
        {
            _heroData = heroData;
            var speed = _speedUpgradeConfig.MoveSpeedTable.GetSpeed(Level);
            _heroData.SetSpeed(speed);
        }

        protected override void OnUpgrade()
        {
            var speed = _speedUpgradeConfig.MoveSpeedTable.GetSpeed(Level);
            _heroData.SetSpeed(speed);
            Debug.Log($"<color=blue>hero Speed Upgraded. New speed: {speed}</color>");
        }
    }
}
