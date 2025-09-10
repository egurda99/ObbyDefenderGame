using Zenject;

namespace ObbyDefender
{
    public interface IEnemy
    {
        AnimalType AnimalType { get; }
        void OnSpawned(IMemoryPool rangeEnemyPool);
        void OnDespawned();
    }
}
