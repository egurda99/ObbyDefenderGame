using System;
using Atomic.Elements;
using UnityEngine;

namespace ObbyDefender.Weapons
{
    [Serializable]
    public sealed class HeroGunsView
    {
        [SerializeField] private WeaponSlot[] _weapons;

        private WeaponSlot _activeWeapon;
        private ReactiveVariable<Transform> _firePoint;

        public WeaponSlot ActiveWeapon => _activeWeapon;


        public void SetActiveWeapon(WeaponType type)
        {
            foreach (var slot in _weapons)
            {
                var isActive = slot.Type == type;
                slot.Weapon.SetActive(isActive);
                //              slot.FirePoint.SetActive(isActive);

                if (isActive)
                    _activeWeapon = slot;
            }
        }

        public Transform GetActiveFirePoint()
        {
            return _activeWeapon?.FirePoint?.transform;
        }
    }
}
