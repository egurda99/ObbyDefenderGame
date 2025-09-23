using _UpgradePractice.Scripts;
using UnityEngine;
using Zenject;

namespace ObbyDefender.Scripts
{
    public sealed class HealthUpgrade : Upgrade
    {
        private readonly HealthUpgradeConfig _speedUpgradeConfig;
        private HeroData _heroData;

        public HealthUpgrade(HealthUpgradeConfig config) : base(config)
        {
            _speedUpgradeConfig = config;
        }

        [Inject]
        public void Construct(HeroData heroData)
        {
            _heroData = heroData;
            SetHealth();
        }

        private void SetHealth()
        {
            var health = _speedUpgradeConfig.HealthTable.GetHealth(Level);
            _heroData.SetHealth(health);
        }

        protected override void OnUpgrade()
        {
            SetHealth();
            Debug.Log($"<color=blue>hero Health Upgraded. New Health: {_heroData.Health}</color>");
        }
    }
}
