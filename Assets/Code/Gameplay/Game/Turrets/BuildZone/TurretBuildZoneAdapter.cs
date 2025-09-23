using System;
using MyCodeBase;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class TurretBuildZoneAdapter
    {
        private readonly TurretBuildZoneView _view;
        private readonly int _price;
        private readonly MoneyStorage _moneyStorage;
        private readonly float _timeForBuild;
        public event Action<Vector3> OnZoneBuildRequested;

        public TurretBuildZoneAdapter(int price, float timeForBuild, TurretBuildZoneView view,
            MoneyStorage moneyStorage)
        {
            _timeForBuild = timeForBuild;
            _moneyStorage = moneyStorage;
            _price = price;
            _view = view;
        }

        public void Start()
        {
            _view.OnBuildCompleted += OnBuildCompleted;
            _view.OnPlayerEntered += OnPlayerEntered;
            _view.OnPlayerExited += OnPlayerExited;

            _view.Init(_timeForBuild);
        }

        private void OnBuildCompleted()
        {
            _moneyStorage.SpendMoney(_price);
            OnZoneBuildRequested?.Invoke(_view.transform.position);
        }

        private void OnPlayerExited()
        {
            _view.SetNormalState();
            _view.StopTimer();
        }

        public void Stop()
        {
            _view.OnBuildCompleted -= OnBuildCompleted;
            _view.OnPlayerEntered -= OnPlayerEntered;
            _view.OnPlayerExited -= OnPlayerExited;
        }


        private void OnPlayerEntered()
        {
            if (_moneyStorage.CanSpendMoney(_price))
            {
                _view.SetBuildState();
                _view.RestartTimer();
            }

            else
            {
                _view.SetNotEnoughMoneyState();
            }
        }
    }
}
