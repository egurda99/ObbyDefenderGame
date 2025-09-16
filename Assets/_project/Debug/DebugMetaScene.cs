using _UpgradePractice.Scripts;
using MyCodeBase;
using ObbyDefender.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _project.Debug
{
    public sealed class DebugMetaScene : MonoBehaviour
    {
        [ShowInInspector] private MoneyStorage _moneyStorage;
        private UpgradesManager _upgradeManager;
        [ShowInInspector] [ReadOnly] private WeaponSwitcher _weaponSwitcher;

        [Inject]
        public void Construct(MoneyStorage moneyStorage,
            UpgradesManager upgradesManager, WeaponSwitcher weaponSwitcher)
        {
            _weaponSwitcher = weaponSwitcher;
            _moneyStorage = moneyStorage;
            _upgradeManager = upgradesManager;

            _moneyStorage.SetupMoney(5000);
        }

        [Button]
        public void SwitchWeapon(WeaponType type)
        {
            _weaponSwitcher.SwitchWeapon(type);
        }

        [Button]
        public bool CanLevelUp(string id)
        {
            return _upgradeManager.CanLevelUp(id);
        }

        [Button]
        public void LevelUp(string id)
        {
            _upgradeManager.LevelUp(id);
        }
    }
}
