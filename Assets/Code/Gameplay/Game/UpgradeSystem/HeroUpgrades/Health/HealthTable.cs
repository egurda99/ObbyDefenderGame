using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender
{
    [Serializable]
    public sealed class HealthTable
    {
        public float HealthStep
        {
            get { return stepHealth; }
        }

        [Space] [InfoBox("Health: Linear Function")] [SerializeField]
        private float startHealth;

        [SerializeField] private float endHealth;

        [ReadOnly] [SerializeField] private float stepHealth;

        [Space]
        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [SerializeField]
        private float[] table;

        public float GetHealth(int level)
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
            table[0] = startHealth;
            table[maxLevel - 1] = endHealth;

            var speedStep = (endHealth - startHealth) / (maxLevel - 1);
            stepHealth = (float)Math.Round(speedStep, 2);

            for (var i = 1; i < maxLevel - 1; i++)
            {
                var Health = startHealth + stepHealth * i;
                table[i] = (float)Math.Round(Health, 2);
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
