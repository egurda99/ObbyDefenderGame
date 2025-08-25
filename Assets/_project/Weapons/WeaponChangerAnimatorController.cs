using System;
using UnityEngine;

namespace ObbyDefender.Weapons
{
    [Serializable]
    public sealed class WeaponChangerAnimatorController
    {
        [SerializeField] private Animator _animator;

        private WeaponType _currentWeapon;

        public void SetWeapon(WeaponType weapon)
        {
            if (_currentWeapon == weapon)
                return;

            if (_currentWeapon != WeaponType.None)
            {
                var oldBool = $"Is{_currentWeapon}";
                _animator.SetBool(oldBool, false);
            }


            if (weapon != WeaponType.None)
            {
                var newBool = $"Is{weapon}";
                _animator.SetBool(newBool, true);
            }

            _currentWeapon = weapon;
        }
    }
}
