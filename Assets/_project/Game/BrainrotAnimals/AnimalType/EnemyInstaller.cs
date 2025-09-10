using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [Header("Melee animals")] [SerializeField]
        private List<MeleeBrainrotAnimalInstaller> _meleePrefabs;

        [Header("Range animals")] [SerializeField]
        private List<RangeBrainrotAnimalInstaller> _rangePrefabs;

        [SerializeField] private Transform _enemyContainer;

        public override void InstallBindings()
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

            Container.Bind<MeleeAnimalFactory>()
                .AsSingle()
                .WithArguments(_meleePrefabs);

            Container.Bind<RangeAnimalFactory>()
                .AsSingle()
                .WithArguments(_rangePrefabs);
        }
    }
}
