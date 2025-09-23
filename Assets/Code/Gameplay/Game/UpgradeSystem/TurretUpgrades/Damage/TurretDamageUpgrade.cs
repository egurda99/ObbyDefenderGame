using _UpgradePractice.Scripts;
using UnityEngine;
using Zenject;

namespace ObbyDefender.Scripts
{
    public sealed class TurretDamageUpgrade : Upgrade
    {
        private readonly TurretDamageUpgradeConfig _turretDamageUpgradeConfig;
        private TurretData _turretData;

        public TurretDamageUpgrade(TurretDamageUpgradeConfig config) : base(config)
        {
            _turretDamageUpgradeConfig = config;
        }

        [Inject]
        public void Construct(TurretData turretData)
        {
            _turretData = turretData;
            SetDamage();
        }

        private void SetDamage()
        {
            var damage = _turretDamageUpgradeConfig.DamageUpgradeTable.GetDamage(Level);
            _turretData.SetDamage(damage);
        }

        protected override void OnUpgrade()
        {
            SetDamage();
            Debug.Log($"<color=blue>turret Damage Upgraded. New damage: {_turretData.Damage}</color>");
        }
    }
}
