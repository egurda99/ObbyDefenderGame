using System;

namespace ObbyDefender.Scripts
{
    [Serializable]
    public sealed class HeroData
    {
        private float _speed;
        private float _health;

        public float Speed => _speed;

        public float Health => _health;

        public HeroData(float speed, float health)
        {
            _speed = speed;
            _health = health;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetHealth(float health)
        {
            _health = health;
        }
    }
}
