using Zenject;

namespace ObbyDefender
{
    public sealed class RangeAnimalPool : MonoMemoryPool<RangeBrainrotAnimalInstaller>
    {
        protected override void OnSpawned(RangeBrainrotAnimalInstaller item)
        {
            base.OnSpawned(item);
            item.OnSpawned(this);
        }

        protected override void OnDespawned(RangeBrainrotAnimalInstaller item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}
