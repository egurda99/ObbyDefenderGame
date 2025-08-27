using System;
using MyCodeBase;

namespace ObbyDefender
{
    public sealed class TurretBuildZoneAdapter
    {
        private readonly TurretBuildZoneView _view;
        private readonly int _price;
        private readonly MoneyStorage _moneyStorage;
        private int _timeForBuild;
        public event Action<TurretBuildZoneView> OnZoneBuildRequested;

        public TurretBuildZoneAdapter(int price, int timeForBuild, TurretBuildZoneView view, MoneyStorage moneyStorage)
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
        }

        private void OnBuildCompleted()
        {
            OnZoneBuildRequested?.Invoke(_view);
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
