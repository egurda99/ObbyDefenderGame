using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class DamageUpgradeTable
    {
        public float DamageStep
        {
            get { return damageStep; }
        }

        [Space] [InfoBox("Damage: Linear Function")] [SerializeField]
        private float startDamage;

        [SerializeField] private float endDamage;

        [ReadOnly] [SerializeField] private float damageStep;

        [Space]
        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [SerializeField]
        private float[] table;

        public float GetDamage(int level)
        {
            var index = Mathf.Clamp(level - 1, 0, table.Length);
            return table[index];
        }

        public void OnValidate(int maxLevel)
        {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel)
        {
            table = new float[maxLevel];
            table[0] = startDamage;
            table[maxLevel - 1] = endDamage;

            var speedStep = (endDamage - startDamage) / (maxLevel - 1);
            damageStep = (float)Math.Round(speedStep, 2);

            for (var i = 1; i < maxLevel - 1; i++)
            {
                var speed = startDamage + damageStep * i;
                table[i] = (float)Math.Round(speed, 2);
            }
        }

#if UNITY_EDITOR
        private void DrawLabelForListElement(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }
#endif
    }
}
