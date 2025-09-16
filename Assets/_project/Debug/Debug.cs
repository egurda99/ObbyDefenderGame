using _UpgradePractice.Scripts;
using MyCodeBase;
using ObbyDefender;
using ObbyDefender.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _project.Debug
{
    public sealed class Debug : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly] private WeaponSwitcher _weaponSwitcher;
        [ShowInInspector] private TurretsManager _turretsManager;
        [ShowInInspector] private MoneyStorage _moneyStorage;
        private UpgradesManager _upgradeManager;

        [Inject]
        public void Construct(WeaponSwitcher weaponSwitcher, TurretsManager turretsManager, MoneyStorage moneyStorage,
            UpgradesManager upgradesManager)
        {
            _moneyStorage = moneyStorage;
            _turretsManager = turretsManager;
            _weaponSwitcher = weaponSwitcher;
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
