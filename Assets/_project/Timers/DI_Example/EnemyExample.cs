
using UnityEngine;
using Zenject;

namespace MyCodeBase.Timers
{
    public class EnemyExample : MonoBehaviour
    {
        private Countdown _attackCountdown;

        [Inject]
        public void Construct(Countdown attackCountdown)
        {
            _attackCountdown = attackCountdown;
            _attackCountdown.Duration = 3f;
            _attackCountdown.OnEnded += OnAttack;
            _attackCountdown.Start();
        }

        private void OnAttack()
        {
            Debug.Log($"{name} атакует!");
            _attackCountdown.Start(); // перезапуск
        }
    }
}
