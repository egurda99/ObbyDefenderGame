using System;

namespace ObbyDefender
{
    public sealed class CurrentWaveViewAdapter : IDisposable
    {
        private readonly ValueView _valueView;
        private readonly WavesSystem _wavesSystem;

        public CurrentWaveViewAdapter(ValueView valueView, WavesSystem wavesSystem)
        {
            _valueView = valueView;
            _wavesSystem = wavesSystem;
            SetViewValue(_wavesSystem.CurrentWaveIndex);

            _wavesSystem.WaveChanged += OnWaveChanged;
        }

        private void OnWaveChanged(int value)
        {
            SetViewValue(value);
        }

        private void SetViewValue(int value)
        {
            _valueView.SetValue("Волна №" + (value + 1));
        }


        public void Dispose()
        {
            _wavesSystem.WaveChanged -= OnWaveChanged;
        }
    }
}
