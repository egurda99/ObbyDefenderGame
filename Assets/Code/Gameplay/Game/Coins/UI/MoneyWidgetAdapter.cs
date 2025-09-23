using System;
using DG.Tweening;
using MyCodeBase;

namespace ObbyDefender
{
    public sealed class MoneyWidgetAdapter : IDisposable
    {
        private readonly MoneyView _currencyView;

        private int _lastCurrency;
        private Tween _tween;
        private readonly MoneyStorage _moneyStorage;
        private const float AnimationDuration = 0.5f;

        public MoneyWidgetAdapter(MoneyView currencyView, MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _currencyView = currencyView;
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;

            Setter(_moneyStorage.Money);
        }

        private void OnMoneyChanged(int value)
        {
            _tween?.Kill();

            var startValue = _lastCurrency;

            _tween = DOTween.To(
                () => startValue,
                value =>
                {
                    startValue = value;
                    _currencyView.UpdateCurrency(value.ToString());
                },
                value,
                AnimationDuration
            ).SetEase(Ease.OutQuad);

            _lastCurrency = value;
        }

        private void Setter(int value)
        {
            _currencyView.UpdateCurrency(value.ToString());
            _lastCurrency = value;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            _tween?.Kill();
        }
    }
}
