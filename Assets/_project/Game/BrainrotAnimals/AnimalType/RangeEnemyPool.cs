using Zenject;

namespace ObbyDefender
{
    public class RangeEnemyPool : MonoMemoryPool<RangeBrainrotAnimalInstaller>
    {
        private EnemyType _enemyType;

        public void Init(EnemyType type)
        {
            _enemyType = type;
        }

        protected override void Reinitialize(RangeBrainrotAnimalInstaller item)
        {
            base.Reinitialize(item);
            item.OnSpawned();
        }

        protected override void OnDespawned(RangeBrainrotAnimalInstaller item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}
