using System;
using MyCodeBase;
using ObbyDefender.Scripts;

namespace ObbyDefender
{
    public sealed class EndGamePopupShower : IDisposable
    {
        private readonly PopupManager _popupManager;
        private readonly GameEndObserver _gameEndObserver;

        public EndGamePopupShower(PopupManager popupManager, GameEndObserver gameEndObserver)
        {
            _popupManager = popupManager;
            _gameEndObserver = gameEndObserver;

            _gameEndObserver.GameLost += OnGameLost;
            _gameEndObserver.GameWon += OnGameWin;
        }

        private void OnGameWin(int waveBonus, int killedEnemies, int collectedMoney)
        {
            var popup = _popupManager.FindPopup(PopupName.GAME_ENDED);

            if (popup is GameEndPopup gameEndPopup)
            {
                gameEndPopup.Init(killedEnemies, waveBonus, collectedMoney);
            }

            _popupManager.ShowPopup(PopupName.GAME_ENDED);
        }

        private void OnGameLost(int enemiesKilled, int collectedMoney)
        {
            var popup = _popupManager.FindPopup(PopupName.GAME_ENDED);

            if (popup is GameEndPopup gameEndPopup)
            {
                gameEndPopup.Init(enemiesKilled, 0, collectedMoney);
            }

            _popupManager.ShowPopup(PopupName.GAME_ENDED);
        }


        public void Dispose()
        {
            _gameEndObserver.GameLost -= OnGameLost;
            _gameEndObserver.GameWon -= OnGameWin;
        }
    }
}
