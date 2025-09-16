using System;
using MyCodeBase;
using ShootEmUp;

namespace ObbyDefender
{
    public sealed class GameEndObserver : IDisposable
    {
        private readonly GameEndHandler _gameEndHandler;
        private readonly GameCycleManager _gameCycleManager;
        private readonly MoneyStorage _moneyStorage;

        public event Action<int, int> GameLost;
        public event Action<int, int, int> GameWon;

        public GameEndObserver(GameEndHandler gameEndHandler, GameCycleManager gameCycleManager,
            MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _gameEndHandler = gameEndHandler;
            _gameCycleManager = gameCycleManager;

            _gameEndHandler.GameLost += OnGameLost;
            _gameEndHandler.GameWon += OnGameWon;
        }

        private void OnGameWon(int reward, int killedEnemies, int collectedMoney)
        {
            _gameCycleManager.PauseGame();
            _moneyStorage.EarnMoney(reward);

            GameWon?.Invoke(reward, killedEnemies, collectedMoney);
        }

        private void OnGameLost(int enemiesKilled, int collectedMoney)
        {
            _gameCycleManager.PauseGame();
            GameLost?.Invoke(enemiesKilled, collectedMoney);
        }

        public void Dispose()
        {
            _gameEndHandler.GameLost -= OnGameLost;
            _gameEndHandler.GameWon -= OnGameWon;
        }
    }
}
