using UnityEngine.Events;

namespace ObbyDefender.Scripts
{
    public interface IEndGameView
    {
        void SetTitleText(string title);
        void AddButtonListener(UnityAction action);
        void RemoveButtonListener(UnityAction action);
        void SetEnemiesText(string enemies);
        void SetMoneyText(string money);
        void SetWaveBonusText(string waveBonus);
        void SetWinBackground();
        void SetLostBackground();
        void HideWaveBonusText();
    }
}
