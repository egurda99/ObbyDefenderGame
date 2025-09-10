using Zenject;

namespace ObbyDefender
{
    public sealed class MeleeAnimalPool : MonoMemoryPool<MeleeBrainrotAnimalInstaller>
    {
        protected override void OnSpawned(MeleeBrainrotAnimalInstaller item)
        {
            base.OnSpawned(item);
            item.OnSpawned(this);
        }

        protected override void OnDespawned(MeleeBrainrotAnimalInstaller item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}
