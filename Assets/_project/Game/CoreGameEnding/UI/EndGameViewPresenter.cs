using UnityEngine.SceneManagement;

namespace ObbyDefender.Scripts
{
    public sealed class EndGameViewPresenter
    {
        private readonly IEndGameView _view;
        private readonly int _enemiesKilled;
        private readonly int _waveBonus;
        private readonly int _collectedMoney;
        private const string META_SCENE = "MetaScene";

        public EndGameViewPresenter(IEndGameView view, int enemiesKilled, int waveBonus, int collectedMoney)
        {
            _collectedMoney = collectedMoney;
            _waveBonus = waveBonus;
            _enemiesKilled = enemiesKilled;
            _view = view;
        }

        public void Start()
        {
            _view.SetEnemiesText(_enemiesKilled.ToString());
            _view.SetMoneyText(_collectedMoney.ToString());


            if (_waveBonus == 0) // lost
            {
                _view.SetTitleText("Game Over :(");
                _view.HideWaveBonusText();
                _view.SetLostBackground();
            }

            else
            {
                _view.SetTitleText("YOU WIN !");

                _view.SetWaveBonusText(_waveBonus.ToString());
                _view.SetWinBackground();
            }

            _view.AddButtonListener(OnContinueClicked);
        }


        public void Stop()
        {
            _view.RemoveButtonListener(OnContinueClicked);
        }

        private void OnContinueClicked()
        {
            SceneManager.LoadScene(META_SCENE);
        }
    }
}
