using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(fileName = "EnemyConfigDatabase", menuName = "Game/Configs/Enemy Config Database")]
    public class EnemyConfigDatabase : ScriptableObject
    {
        [TableList] public List<EnemyConfig> Enemies = new();

        [Serializable]
        public class EnemyConfig
        {
            [TableColumnWidth(120, Resizable = false)] [LabelText("ID")]
            public AnimalType EnemyId;

            [TableColumnWidth(100, Resizable = false)] [LabelText("Òèï")]
            public AnimalAttackType EnemyType;

            [InlineProperty] [HideLabel] public EnemyBaseBalanceStats BaseBalanceStats;

            [Header("Prefab")] [PreviewField] [HideLabel]
            public GameObject Prefab;
        }

        public GameObject GetPrefab(AnimalType id)
        {
            var config = Enemies.Find(e => e.EnemyId == id);
            return config?.Prefab;
        }
    }
}
