using ObbyDefender.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _project.Debug
{
    public class GunsSwithcherDebug : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly] private WeaponSwitcher _weaponSwitcher;

        [Inject]
        public void Construct(WeaponSwitcher weaponSwitcher)
        {
            _weaponSwitcher = weaponSwitcher;
        }

        [Button]
        public void SwitchWeapon(WeaponType type)
        {
            _weaponSwitcher.SwitchWeapon(type);
        }
    }
}
