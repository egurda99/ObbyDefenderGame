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

        [Inject]
        public void Construct(WeaponSwitcher weaponSwitcher, TurretsManager turretsManager, MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _turretsManager = turretsManager;
            _weaponSwitcher = weaponSwitcher;

            _moneyStorage.SetupMoney(5000);
        }

        [Button]
        public void SwitchWeapon(WeaponType type)
        {
            _weaponSwitcher.SwitchWeapon(type);
        }
    }
}
