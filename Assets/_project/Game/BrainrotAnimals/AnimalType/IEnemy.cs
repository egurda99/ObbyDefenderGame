namespace ObbyDefender
{
    public interface IEnemy
    {
        EnemyType EnemyType { get; }
        void OnSpawned(MeleeEnemyPool meleeEnemyPool);
        void OnDespawned();
    }
}
