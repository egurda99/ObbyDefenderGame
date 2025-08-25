
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyCodeBase.Timers
{
    public sealed class DemoTimers : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly] private Countdown _countdown;
        [ShowInInspector] [ReadOnly] private Timer _timer;
        [ShowInInspector] [ReadOnly] private Cycle _cycle;
        [ShowInInspector] [ReadOnly] private Stopwatch _stopwatch;

        private void Start()
        {
            // Countdown: отсчёт вниз от 5 секунд
            _countdown = new Countdown(5f);
            _countdown.OnEnded += () => Debug.Log("[Countdown] Закончил отсчёт!");
            _countdown.Start();

            // Timer: отсчёт вверх до 3 секунд
            _timer = new Timer(3f);
            _timer.OnEnded += () => Debug.Log("[Timer] Достиг лимита!");
            _timer.Start();

            // Cycle: каждые 2 секунды вызывает OnCycle
            _cycle = new Cycle(2f);
            _cycle.OnCycle += () => Debug.Log("[Cycle] Новый цикл!");
            _cycle.Start();

            // Stopwatch: просто считает время с момента старта
            _stopwatch = new Stopwatch();
            _stopwatch.OnCurrentTimeChanged += t =>
            {
                if (Mathf.FloorToInt(t) % 5 == 0) // каждые 5 секунд
                {
                    Debug.Log($"[Stopwatch] прошло {Mathf.FloorToInt(t)} секунд");
                }
            };
            _stopwatch.Start();
        }

        private void Update()
        {
            var dt = Time.deltaTime;

            _countdown.Tick(dt);
            _timer.Tick(dt);
            _cycle.Tick(dt);
            _stopwatch.Tick(dt);
        }
    }
}
