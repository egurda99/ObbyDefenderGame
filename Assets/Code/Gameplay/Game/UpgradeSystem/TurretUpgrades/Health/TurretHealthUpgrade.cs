using _UpgradePractice.Scripts;
using ObbyDefender.Scripts;
using UnityEngine;
using Zenject;

namespace ObbyDefender.TurretUpgrades
{
    public sealed class TurretHealthUpgrade : Upgrade
    {
        private readonly TurretHealthUpgradeConfig _speedUpgradeConfig;
        private TurretData _turretData;

        public TurretHealthUpgrade(TurretHealthUpgradeConfig config) : base(config)
        {
            _speedUpgradeConfig = config;
        }

        [Inject]
        public void Construct(TurretData turretData)
        {
            _turretData = turretData;
            SetHealth();
        }

        private void SetHealth()
        {
            var health = _speedUpgradeConfig._turretHealthTable.GetHealth(Level);
            _turretData.SetHealth(health);
        }

        protected override void OnUpgrade()
        {
            SetHealth();
            Debug.Log($"<color=blue>Turret Health Upgraded. New Health: {_turretData.Health}</color>");
        }
    }
}
