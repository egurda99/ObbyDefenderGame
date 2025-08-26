using Atomic.Entities;
using Elementary;
using ObbyDefender.Weapons;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ObbyDefender.DI
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private SceneInstallerHelper _sceneInstallerHelper;


        public override void InstallBindings()
        {
            BindBulletPool();
            BindCycleManager();
            BindPlayer();
            BindWeaponSwitcher();
            BindInput();
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

            Container.Bind<BulletFactory>().AsSingle();
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

            var sensor = player.GetComponentInChildren<ColliderDetectionOverlapSphere>();

            Container.BindInterfacesTo<NearestTargetObserver>().AsSingle().WithArguments(sensor, player);
        }

        private void BindWeaponSwitcher()
        {
            Container.Bind<WeaponSwitcher>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<HeroInputController>().AsSingle();
        }
    }
}
