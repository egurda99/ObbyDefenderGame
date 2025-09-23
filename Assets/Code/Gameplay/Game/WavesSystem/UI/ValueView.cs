using TMPro;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class ValueView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valueText;

        public void SetValue(string value)
        {
            _valueText.text = value;
        }
    }
}
