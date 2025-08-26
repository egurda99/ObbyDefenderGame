using Atomic.Elements;
using Atomic.Entities;
using ObbyDefender.DI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender.Weapons
{
    public sealed class WeaponSwitcher
    {
        private readonly HeroGunsView _gunsView;
        private readonly WeaponChangerAnimatorController _changerAnimatorController;
        [ShowInInspector] [ReadOnly] private WeaponType _currentWeapon = WeaponType.None;
        private readonly ReactiveVariable<Transform> _firePoint;
        private readonly ReactiveVariable<GameObject> _bulletPrefab;


        public WeaponSwitcher(PlayerService playerService)
        {
            _gunsView = playerService.Player.GetHeroGunsView();
            _changerAnimatorController = playerService.Player.GetWeaponChangerAnimator();
            _firePoint = playerService.Player.GetFirePoint();
            _bulletPrefab = playerService.Player.GetBulletPrefab();


            SwitchWeapon(WeaponType.Pistol);
        }

        [Button]
        public void SwitchWeapon(WeaponType type)
        {
            if (_currentWeapon == type)
                return;

            _gunsView.SetActiveWeapon(type);
            _firePoint.Value = _gunsView.ActiveWeapon.FirePoint.transform;
            _bulletPrefab.Value = _gunsView.ActiveWeapon.BulletPrefab;

            _changerAnimatorController.SetWeapon(type);

            _currentWeapon = type;
        }

        public Transform GetActiveFirePoint()
        {
            return _gunsView.GetActiveFirePoint();
        }
    }
}
