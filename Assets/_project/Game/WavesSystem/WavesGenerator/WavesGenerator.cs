using System.Collections.Generic;

namespace ObbyDefender
{
    public sealed class WavesGenerator
    {
        private readonly int _waveCount;
        private List<Wave> _waves = new();
        private readonly WaveBalancer _waveBalancer;

        public int WaveCount => _waveCount;

        public List<Wave> Waves => _waves;

        public WavesGenerator(int waveCount)
        {
            _waveCount = waveCount;
        }

        public WavesGenerator(int waveCount, WaveBalancer waveBalancer)
        {
            _waveBalancer = waveBalancer;
            _waveCount = waveCount;
        }

        public void Generate(WaveBalancer balancer)
        {
            _waves = balancer.GenerateWaves(_waveCount);
        }

        public void Generate()
        {
            _waves = _waveBalancer.GenerateWaves(_waveCount);
        }
    }
}
