using System;

namespace ObbyDefender
{
    [Serializable]
    public class EnemyEntry
    {
        public AnimalType EnemyId;
        public int Count;
        public EnemyStats Stats; // "снимок" статов на момент волны
    }
}