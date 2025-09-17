using System.Collections.Generic;
using Atomic.Entities;
using MyCodeBase;
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
            BindPlayerUpgradeApplier();
            BindWeaponSwitcher();
            BindInput();
            BindTurrets();
            ConfigureBase();

            BindEnemyConfigDatabase();

            BindAnimalPools();
            BindAnimalFactories();

            BindWavesModule();

            BindCoinsModule();
            ConfigureHUD();
            BindPopupManager();
            BindGameEnd();
            BindSaveLoaderUpdater();
        }

        private void BindSaveLoaderUpdater()
        {
            Container.BindInterfacesAndSelfTo<SceneContainerUpdater>().AsSingle().NonLazy();
        }

        private void BindPopupManager()
        {
            Container.Bind<PopupManager>().FromComponentInHierarchy().AsSingle();
        }

        private void BindGameEnd()
        {
            Container.BindInterfacesAndSelfTo<GameEndHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEndObserver>().AsSingle();
            Container.BindInterfacesAndSelfTo<EndGamePopupShower>().AsSingle();
        }

        private void BindPlayerUpgradeApplier()
        {
            Container.BindInterfacesAndSelfTo<HeroUpgradesApplier>().AsSingle();
        }

        private void ConfigureHUD()
        {
            Container.BindInterfacesTo<MoneyWidgetAdapter>().AsSingle().WithArguments(_sceneInstallerHelper.MoneyView);
        }


        private void BindCoinsModule()
        {
            Container.BindMemoryPool<CoinInstaller, CoinsPool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_sceneInstallerHelper.CoinPrefab)
                .UnderTransform(_sceneInstallerHelper.CoinsContainer);

            Container.BindInterfacesAndSelfTo<CoinsSpawner>().AsSingle()
                .WithArguments(_sceneInstallerHelper.CoinsBalanceConfig);
        }

        private void ConfigureBase()
        {
            var baseTransform = Instantiate(_sceneInstallerHelper.BasePrefab,
                _sceneInstallerHelper.BaseSpawnPoint.position,
                Quaternion.identity, _sceneInstallerHelper.BaseContainer);

            Container.Bind<BaseService>().AsSingle().WithArguments(baseTransform.GetComponent<SceneEntity>());
        }

        private void BindTurrets()
        {
            Container.BindInterfacesAndSelfTo<TurretBuilderSystem>().AsSingle().WithArguments(
                _sceneInstallerHelper.TurretsConfig, _sceneInstallerHelper.TurretBuildZoneView,
                _sceneInstallerHelper.TurretZoneContainer);

            Container.Bind<TurretsManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<TurretSpawner>().AsSingle()
                .WithArguments(_sceneInstallerHelper.TurretZoneContainer, _sceneInstallerHelper.TurretPrefab).NonLazy();
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

        private void BindWeaponSwitcher()
        {
            Container.Bind<WeaponSwitcher>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<HeroInputController>().AsSingle();
        }

        private void BindEnemyConfigDatabase()
        {
            Container.Bind<EnemyConfigDatabase>().FromInstance(_sceneInstallerHelper.EnemyConfigDatabase).AsSingle();
        }

        private void BindWavesModule()
        {
            var meleeAttackAnimals = new List<AnimalType>();
            foreach (var e in _sceneInstallerHelper.EnemyConfigDatabase.Enemies)
            {
                if (e.EnemyType == AnimalAttackType.Melee)
                    meleeAttackAnimals.Add(e.EnemyId);
            }

            var rangeAttackAnimals = new List<AnimalType>();
            foreach (var e in _sceneInstallerHelper.EnemyConfigDatabase.Enemies)
            {
                if (e.EnemyType == AnimalAttackType.Range)
                    rangeAttackAnimals.Add(e.EnemyId);
            }

            var animalTypes = new AnimalTypesHolder
            {
                MeleeAttackAnimals = meleeAttackAnimals,

                RangeAttackAnimals = rangeAttackAnimals
            };


            var balancer = new WaveBalancer(_sceneInstallerHelper.EnemyConfigDatabase,
                _sceneInstallerHelper.FormulaConfig);

            var configurator = new WavesGenerator(_sceneInstallerHelper.WaveCount, balancer);
            configurator.Generate();

            Container.Bind<WavesSystem>().AsSingle().WithArguments(configurator.Waves);

            Container.BindInterfacesAndSelfTo<AnimalSpawner>().AsSingle()
                .WithArguments(_sceneInstallerHelper.SpawnZones, _sceneInstallerHelper.BasePosition,
                    _sceneInstallerHelper.MinSpawnDelay,
                    _sceneInstallerHelper.MaxSpawnDelay, animalTypes);

            Container.BindInterfacesAndSelfTo<ActiveEnemiesProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesRemainingHandler>().AsSingle();

            Container.BindInterfacesTo<EnemiesRemainingAdapter>().AsSingle()
                .WithArguments(_sceneInstallerHelper.RemainingEnemiesView);
        }

        private void BindAnimalFactories()
        {
            Container.Bind<MeleeAnimalFactory>().AsSingle();
            Container.Bind<RangeAnimalFactory>().AsSingle();
        }

        private void BindAnimalPools()
        {
            foreach (var config in _sceneInstallerHelper.EnemyConfigDatabase.Enemies)
            {
                if (config.Prefab == null)
                {
                    Debug.LogError($"Prefab not set for {config.EnemyId} in EnemyConfigDatabase!");
                    continue;
                }

                if (config.EnemyType == AnimalAttackType.Melee)
                {
                    Container.BindMemoryPool<MeleeBrainrotAnimalInstaller, MeleeAnimalPool>()
                        .WithId(config.EnemyId)
                        .WithInitialSize(5)
                        .FromComponentInNewPrefab(config.Prefab)
                        .UnderTransform(_sceneInstallerHelper.EnemyContainer)
                        .AsCached()
                        .NonLazy();
                }

                else if (config.EnemyType == AnimalAttackType.Range)
                {
                    Container.BindMemoryPool<RangeBrainrotAnimalInstaller, RangeAnimalPool>()
                        .WithId(config.EnemyId)
                        .WithInitialSize(3)
                        .FromComponentInNewPrefab(config.Prefab)
                        .UnderTransform(_sceneInstallerHelper.EnemyContainer)
                        .AsCached()
                        .NonLazy();
                }
            }
        }
    }
}
