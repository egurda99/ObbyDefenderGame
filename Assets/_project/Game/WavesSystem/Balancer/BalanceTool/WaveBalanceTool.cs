using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(fileName = "WaveBalanceTool", menuName = "Game/Tools/Wave Balance Tool")]
    public class WaveBalanceTool : ScriptableObject
    {
        [Header("Настройки")] public EnemyConfigDatabase EnemyDatabase;
        public WaveModuleBalancerConfig FormulaConfig;

        [Header("Генератор")] public int WaveCount = 10;

        private WavesGenerator _configurator;
        private WaveBalancer _balancer;

        [Button("Generate Waves")]
        private void Generate()
        {
            _balancer ??= new WaveBalancer(EnemyDatabase, FormulaConfig);
            _configurator = new WavesGenerator(WaveCount);
            _configurator.Generate(_balancer);
        }

        [ShowInInspector]
        [TableList]
        private List<Wave> WavesPreview
        {
            get { return _configurator?.Waves; }
        }
    }
}