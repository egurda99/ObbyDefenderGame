using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public class EnemyInstaller : MonoInstaller
    {
        [Header("Ближние враги")] [SerializeField]
        private List<MeleeBrainrotAnimalInstaller> _meleePrefabs;

        [SerializeField] private Transform _enemyContainer;

        public override void InstallBindings()
        {
            // Создаём пулы для каждого типа ближника с уникальным ID
            foreach (var prefab in _meleePrefabs)
            {
                Container.BindMemoryPool<MeleeBrainrotAnimalInstaller, MeleeEnemyPool>()
                    .WithId(prefab.EnemyType)
                    .WithInitialSize(3)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransform(_enemyContainer)
                    .AsCached();
            }

            // Биндим фабрику и передаём только префабы, контейнер внедряется автоматически
            Container.Bind<MeleeEnemyFactory>()
                .AsSingle()
                .WithArguments(_meleePrefabs);
        }
    }
}
