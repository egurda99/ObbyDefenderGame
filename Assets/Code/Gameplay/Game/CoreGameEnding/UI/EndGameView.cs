using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ObbyDefender.Scripts
{
    public sealed class EndGameView : MonoBehaviour, IEndGameView
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _enemiesText;
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _waveBonusValueText;
        [SerializeField] private TextMeshProUGUI _waveBonusText;


        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;

        [SerializeField] private Button _continueButton;

        public void SetTitleText(string title)
        {
            _titleText.text = title;
        }

        public void AddButtonListener(UnityAction action)
        {
            _continueButton.onClick.AddListener(action);
        }

        public void RemoveButtonListener(UnityAction action)
        {
            _continueButton.onClick.RemoveListener(action);
        }

        public void SetEnemiesText(string enemies)
        {
            _enemiesText.text = enemies;
        }

        public void SetMoneyText(string money)
        {
            _moneyText.text = money;
        }

        public void SetWaveBonusText(string waveBonus)
        {
            _waveBonusValueText.text = waveBonus;
        }

        public void SetWinBackground()
        {
            _backgroundImage.color = _winColor;
        }

        public void SetLostBackground()
        {
            _backgroundImage.color = _loseColor;
        }

        public void HideWaveBonusText()
        {
            _waveBonusValueText.gameObject.SetActive(false);
            _waveBonusText.gameObject.SetActive(false);
        }
    }
}
