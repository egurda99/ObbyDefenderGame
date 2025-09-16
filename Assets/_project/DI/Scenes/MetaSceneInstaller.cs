using Atomic.Entities;
using MyCodeBase;
using ObbyDefender.Weapons;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ObbyDefender.DI
{
    public sealed class MetaSceneInstaller : MonoInstaller<MetaSceneInstaller>
    {
        [SerializeField] private MetaSceneInstallerHelper _sceneInstallerHelper;

        public override void InstallBindings()
        {
            BindCycleManager();
            BindBulletPool();
            BindPlayer();
            BindWeaponSwitcher();
            BindPlayerUpgradeApplier();
            BindInput();
            ConfigureHUD();

            BindPopupManager();
        }

        private void ConfigureHUD()
        {
            Container.BindInterfacesTo<MoneyWidgetAdapter>().AsSingle().WithArguments(_sceneInstallerHelper.MoneyView);
        }

        private void BindPopupManager()
        {
            Container.Bind<PopupManager>().FromComponentInHierarchy().AsSingle();
        }

        private void BindPlayerUpgradeApplier()
        {
            Container.BindInterfacesAndSelfTo<HeroUpgradesApplier>().AsSingle();
        }

        private void BindWeaponSwitcher()
        {
            Container.Bind<WeaponSwitcher>().AsSingle();
        }

        private void BindCycleManager()
        {
            Container.Bind<GameCycleManager>().FromComponentInHierarchy().AsSingle();
        }


        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<SceneEntity>(_sceneInstallerHelper.PlayerPrefab,
                _sceneInstallerHelper.SpawnPoint.position,
                Quaternion.identity, _sceneInstallerHelper.PlayerContainer);

            Container.Bind<PlayerService>().AsSingle().WithArguments(player);

            _sceneInstallerHelper.PlayerCamera.Follow = player.transform;
        }

        private void BindBulletPool()
        {
            Container.BindMemoryPool<BulletInstaller, BulletPool>()
                .WithId(WeaponType.Pistol)
                .WithInitialSize(25)
                .FromComponentInNewPrefab(_sceneInstallerHelper.PistolBulletPrefab)
                .UnderTransform(_sceneInstallerHelper.BulletContainer);

            Container.BindMemoryPool<BulletInstaller, BulletPool>()
                .WithId(WeaponType.M16)
                .WithInitialSize(25)
                .FromComponentInNewPrefab(_sceneInstallerHelper.M16BulletPrefab)
                .UnderTransform(_sceneInstallerHelper.BulletContainer);

            Container.BindMemoryPool<BulletInstaller, BulletPool>()
                .WithId(WeaponType.RangeAnimal)
                .WithInitialSize(25)
                .FromComponentInNewPrefab(_sceneInstallerHelper.RangeAnimalBulletPrefab)
                .UnderTransform(_sceneInstallerHelper.BulletContainer);

            Container.Bind<BulletFactory>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<HeroInputController>().AsSingle();
        }
    }
}
