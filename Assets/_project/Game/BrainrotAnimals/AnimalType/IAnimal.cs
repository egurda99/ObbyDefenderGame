using Zenject;

namespace ObbyDefender
{
    public interface IAnimal
    {
        AnimalType AnimalType { get; }
        void OnSpawned(IMemoryPool rangeEnemyPool);
        void OnDespawned();
    }
}
