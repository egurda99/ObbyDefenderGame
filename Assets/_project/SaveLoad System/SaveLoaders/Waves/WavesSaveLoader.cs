using System;
using ObbyDefender;
using UnityEngine;

namespace MyCodeBase
{
    [Serializable]
    public sealed class WavesSaveLoader : SaveLoader<WavesSystem, WaveData>
    {
        protected override WaveData ConvertToData(WavesSystem service)
        {
            Debug.Log($"<color=yellow>Converted to data wave number = {service.CurrentWaveIndex}</color>");

            var waveData = new WaveData(service.CurrentWaveIndex);

            return waveData;
        }

        protected override void SetupData(WavesSystem service, WaveData data)
        {
            Debug.Log($"<color=yellow>Setuped wave = {data.LastEndedWave}</color>");

            service.SetWave(data.LastEndedWave + 1);
        }

        protected override void SetupDefaultData(WavesSystem service)
        {
            Debug.Log("<color=yellow>Setup default start wave</color>");
            service.SetWave(1);
        }
    }
}
