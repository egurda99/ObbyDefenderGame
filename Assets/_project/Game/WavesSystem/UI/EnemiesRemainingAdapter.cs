using System;

namespace ObbyDefender
{
    public sealed class EnemiesRemainingAdapter : IDisposable
    {
        private readonly ValueView _valueView;
        private readonly EnemiesRemainingHandler _handler;

        public EnemiesRemainingAdapter(ValueView valueView, EnemiesRemainingHandler handler)
        {
            _valueView = valueView;
            _handler = handler;
            SetViewValue(_handler.EnemiesRemaining);

            _handler.OnEnemiesChanged += OnEnemiesChanged;
        }

        private void OnEnemiesChanged(int value)
        {
            SetViewValue(value);
        }

        private void SetViewValue(int value)
        {
            _valueView.SetValue(value.ToString());
        }


        public void Dispose()
        {
            _handler.OnEnemiesChanged -= OnEnemiesChanged;
        }
    }
}