using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [Header("MeleeEntity animals")] [SerializeField]
        private List<MeleeBrainrotAnimalInstaller> _meleePrefabs;

        [Header("Range animals")] [SerializeField]
        private List<RangeBrainrotAnimalInstaller> _rangePrefabs;

        [SerializeField] private Transform _enemyContainer;

        [Header("Spawn and Waves systems")] [Header("Настройки")] [SerializeField]
        private EnemyConfigDatabase _enemyConfigDatabase;

        [SerializeField] private BalanceFormulaConfig _formulaConfig;
        [SerializeField] private int _waveCount;
        [SerializeField] private List<AnimalSpawnZone> _spawnZones;
        [SerializeField] private Transform _basePosition;
        [SerializeField] private float _minSpawnDelay;
        [SerializeField] private float _maxSpawnDelay;


        public override void InstallBindings()
        {
            BindMeleePool();

            BindRangePool();

            BindMeleeFactory();

            BindRangeFactory();


            var animalTypes = new AnimalTypesHolder
            {
                MeleeAttackAnimals = new List<AnimalType>
                {
                    AnimalType.Ballerina,
                    AnimalType.DinDinDon,
                    AnimalType.Gorillo,
                    AnimalType.Mateo,
                    AnimalType.Sahur,
                    AnimalType.Capuchino
                },
                RangeAttackAnimals = new List<AnimalType>
                {
                    AnimalType.Bombardini,
                    AnimalType.Bombardiro,
                    AnimalType.Orcaleo
                }
            };

            //Container.Bind<WaveBalancer>().AsSingle().WithArguments(_formulaConfig, _enemyConfigDatabase);
            var balancer = new WaveBalancer(_enemyConfigDatabase, _formulaConfig);

            var configurator = new WaveConfigurator(_waveCount, balancer);
            configurator.Generate();

            Container.Bind<WaveSystem>().AsSingle().WithArguments(configurator.Waves);

            Container.BindInterfacesAndSelfTo<AnimalSpawner>().AsSingle()
                .WithArguments(_spawnZones, _basePosition, _minSpawnDelay, _maxSpawnDelay, animalTypes);
            //Container.Bind<WaveConfigurator>().AsSingle().WithArguments(_waveCount);
        }

        private void BindRangeFactory()
        {
            Container.Bind<RangeAnimalFactory>()
                .AsSingle()
                .WithArguments(_rangePrefabs);
        }

        private void BindMeleeFactory()
        {
            Container.Bind<MeleeAnimalFactory>()
                .AsSingle()
                .WithArguments(_meleePrefabs);
        }

        private void BindRangePool()
        {
            foreach (var prefab in _rangePrefabs)
            {
                Container.BindMemoryPool<RangeBrainrotAnimalInstaller, RangeAnimalPool>()
                    .WithId(prefab.AnimalType)
                    .WithInitialSize(3)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransform(_enemyContainer)
                    .AsCached()
                    .NonLazy();
            }
        }

        private void BindMeleePool()
        {
            foreach (var prefab in _meleePrefabs)
            {
                Container.BindMemoryPool<MeleeBrainrotAnimalInstaller, MeleeAnimalPool>()
                    .WithId(prefab.AnimalType)
                    .WithInitialSize(3)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransform(_enemyContainer)
                    .AsCached()
                    .NonLazy();
            }
        }
    }
}
