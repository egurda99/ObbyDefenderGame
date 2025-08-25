
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
            // Countdown: ������ ���� �� 5 ������
            _countdown = new Countdown(5f);
            _countdown.OnEnded += () => Debug.Log("[Countdown] �������� ������!");
            _countdown.Start();

            // Timer: ������ ����� �� 3 ������
            _timer = new Timer(3f);
            _timer.OnEnded += () => Debug.Log("[Timer] ������ ������!");
            _timer.Start();

            // Cycle: ������ 2 ������� �������� OnCycle
            _cycle = new Cycle(2f);
            _cycle.OnCycle += () => Debug.Log("[Cycle] ����� ����!");
            _cycle.Start();

            // Stopwatch: ������ ������� ����� � ������� ������
            _stopwatch = new Stopwatch();
            _stopwatch.OnCurrentTimeChanged += t =>
            {
                if (Mathf.FloorToInt(t) % 5 == 0) // ������ 5 ������
                {
                    Debug.Log($"[Stopwatch] ������ {Mathf.FloorToInt(t)} ������");
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
