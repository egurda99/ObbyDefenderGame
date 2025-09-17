using System;

namespace MyCodeBase
{
    [Serializable]
    public sealed class WaveData
    {
        public int LastEndedWave;


        public WaveData(int lastEndedWave)
        {
            LastEndedWave = lastEndedWave;
        }
    }
}
