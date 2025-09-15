using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        // [Header("MeleeEntity animals")] [SerializeField]
        // private List<MeleeBrainrotAnimalInstaller> _meleePrefabs;
        //
        // [Header("Range animals")] [SerializeField]
        // private List<RangeBrainrotAnimalInstaller> _rangePrefabs;

        [SerializeField] private Transform _enemyContainer;

        [Header("Spawn and Waves systems")] [Header("Настройки")] [SerializeField]
        private EnemyConfigDatabase _enemyConfigDatabase;

        [SerializeField] private BalanceFormulaConfig _formulaConfig;
        [SerializeField] private int _waveCount;
        [SerializeField] private List<AnimalSpawnZone> _spawnZones;
        [SerializeField] private Transform _basePosition;
        [SerializeField] private float _minSpawnDelay;
        [SerializeField] private float _maxSpawnDelay;

        [SerializeField] private ValueView _remainingEnemiesView;


        public override void InstallBindings()
        {
            BindEnemyConfigDatabase();

            BindAnimalPools();
            BindAnimalFactories();

            BindWavesModule();
        }

        private void BindEnemyConfigDatabase()
        {
            Container.Bind<EnemyConfigDatabase>().FromInstance(_enemyConfigDatabase).AsSingle();
        }

        private void BindWavesModule()
        {
            var meleeAttackAnimals = new List<AnimalType>();
            foreach (var e in _enemyConfigDatabase.Enemies)
            {
                if (e.EnemyType == AnimalAttackType.Melee)
                    meleeAttackAnimals.Add(e.EnemyId);
            }

            var rangeAttackAnimals = new List<AnimalType>();
            foreach (var e in _enemyConfigDatabase.Enemies)
            {
                if (e.EnemyType == AnimalAttackType.Range)
                    rangeAttackAnimals.Add(e.EnemyId);
            }

            var animalTypes = new AnimalTypesHolder
            {
                MeleeAttackAnimals = meleeAttackAnimals,

                RangeAttackAnimals = rangeAttackAnimals
            };


            var balancer = new WaveBalancer(_enemyConfigDatabase, _formulaConfig);

            var configurator = new WavesGenerator(_waveCount, balancer);
            configurator.Generate();

            Container.Bind<WavesSystem>().AsSingle().WithArguments(configurator.Waves);

            Container.BindInterfacesAndSelfTo<AnimalSpawner>().AsSingle()
                .WithArguments(_spawnZones, _basePosition, _minSpawnDelay, _maxSpawnDelay, animalTypes);

            Container.BindInterfacesAndSelfTo<ActiveEnemiesProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesRemainingHandler>().AsSingle();

            Container.BindInterfacesTo<EnemiesRemainingAdapter>().AsSingle().WithArguments(_remainingEnemiesView);
        }

        private void BindAnimalFactories()
        {
            Container.Bind<MeleeAnimalFactory>().AsSingle();
            Container.Bind<RangeAnimalFactory>().AsSingle();
        }

        private void BindAnimalPools()
        {
            foreach (var config in _enemyConfigDatabase.Enemies)
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
                        .UnderTransform(_enemyContainer)
                        .AsCached()
                        .NonLazy();
                }

                else if (config.EnemyType == AnimalAttackType.Range)
                {
                    Container.BindMemoryPool<RangeBrainrotAnimalInstaller, RangeAnimalPool>()
                        .WithId(config.EnemyId)
                        .WithInitialSize(3)
                        .FromComponentInNewPrefab(config.Prefab)
                        .UnderTransform(_enemyContainer)
                        .AsCached()
                        .NonLazy();
                }
            }
        }
    }
}
