using System;

namespace ObbyDefender.Scripts
{
    [Serializable]
    public sealed class TurretData
    {
        private float _health;
        private float _damage;

        public float Health => _health;

        public float Damage => _damage;

        public TurretData(float health, float damage)
        {
            _health = health;
            _damage = damage;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void SetHealth(float health)
        {
            _health = health;
        }
    }
}
