using System;
using MyCodeBase;

namespace ObbyDefender
{
    public sealed class MoneyWidgetAdapter : IDisposable
    {
        private readonly ValueView _valueView;
        private readonly MoneyStorage _moneyStorage;

        public MoneyWidgetAdapter(ValueView valueView, MoneyStorage moneyStorage)
        {
            _valueView = valueView;
            _moneyStorage = moneyStorage;
            SetViewValue(_moneyStorage.Money);

            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged(int value)
        {
            SetViewValue(value);
        }

        private void SetViewValue(int value)
        {
            _valueView.SetValue(value.ToString());
        }


        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }
    }
}
