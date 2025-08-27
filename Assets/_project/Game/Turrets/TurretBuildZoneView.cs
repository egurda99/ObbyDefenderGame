using System;
using Atomic.Entities;
using MyCodeBase.Timers;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class TurretBuildZoneView : MonoBehaviour
    {
        public event Action OnPlayerEntered;
        public event Action OnPlayerExited;
        public event Action OnBuildCompleted;


        private Timer _timer;
        private int _timeForBuild;

        public void Init(int timeForBuild)
        {
            _timeForBuild = timeForBuild;
            _timer = new Timer(_timeForBuild);
            _timer.OnEnded += OnTimerEnded;
        }

        private void OnTimerEnded()
        {
            OnBuildCompleted?.Invoke();
        }

        public void RestartTimer()
        {
            _timer.SetDuration(_timeForBuild);
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }


        public void SetNotEnoughMoneyState()
        {
            // TODO
        }

        public void SetBuildState()
        {
            // TODO
        }

        public void SetNormalState()
        {
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _timer.Tick(deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    OnPlayerEntered?.Invoke();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    OnPlayerExited?.Invoke();
                }
            }
        }
    }
}
