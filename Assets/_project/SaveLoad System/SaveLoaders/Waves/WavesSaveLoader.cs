using System;
using ObbyDefender;
using UnityEngine;

namespace MyCodeBase
{
    [Serializable]
    public sealed class WavesSaveLoader : SaveLoader<WavesSystem, WaveData>
    {
        private GameEndObserver _gameEndObserver;

        public WavesSaveLoader(GameEndObserver gameEndObserver)
        {
            _gameEndObserver = gameEndObserver;
        }

        protected override WaveData ConvertToData(WavesSystem service)
        {
            Debug.Log($"<color=yellow>Converted to data wave number = {service.CurrentWaveIndex}</color>");

            if (_gameEndObserver.IsGameLost)
            {
                var waveData = new WaveData(service.CurrentWaveIndex - 1);
                return waveData;
            }

            else
            {
                var waveData = new WaveData(service.CurrentWaveIndex);
                return waveData;
            }
        }

        protected override void SetupData(WavesSystem service, WaveData data)
        {
            Debug.Log($"<color=yellow>Setuped wave = {data.LastEndedWave}</color>");

            service.SetWave(data.LastEndedWave);
            service.MoveToNextWave();
        }

        protected override void SetupDefaultData(WavesSystem service)
        {
            Debug.Log("<color=yellow>Setup default start wave</color>");
            service.SetWave(0);
        }
    }
}
