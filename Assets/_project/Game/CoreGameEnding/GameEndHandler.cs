using System;
using Atomic.Entities;
using ObbyDefender.DI;

namespace ObbyDefender
{
    public sealed class GameEndHandler : IDisposable
    {
        private readonly EnemiesRemainingHandler _enemiesRemainingHandler;
        private readonly PlayerService _playerService;
        private readonly BaseService _baseService;

        public event Action<int, int> GameLost;
        public event Action<int, int, int> GameWon;

        public GameEndHandler(EnemiesRemainingHandler enemiesRemainingHandler, PlayerService playerService,
            BaseService baseService)
        {
            _enemiesRemainingHandler = enemiesRemainingHandler;
            _playerService = playerService;
            _baseService = baseService;

            _enemiesRemainingHandler.WaveEnded += OnWaveEnded;
            _playerService.Player.GetIsDead().Subscribe(OnPlayerIsDeadChanged);
            _baseService.Base.GetIsDead().Subscribe(OnBaseDefeated);
        }

        private void OnBaseDefeated(bool value)
        {
            if (value)
            {
                var collectedMoney = _playerService.Player.GetCollectedPoints().Value;

                GameLost?.Invoke(_enemiesRemainingHandler.GetKilledEnemies(), collectedMoney);
            }
        }

        private void OnPlayerIsDeadChanged(bool value)
        {
            if (value)
            {
                var collectedMoney = _playerService.Player.GetCollectedPoints().Value;
                GameLost?.Invoke(_enemiesRemainingHandler.GetKilledEnemies(), collectedMoney);
            }
        }

        private void OnWaveEnded(int reward)
        {
            var collectedMoney = _playerService.Player.GetCollectedPoints().Value;

            GameWon?.Invoke(reward, _enemiesRemainingHandler.GetKilledEnemies(), collectedMoney);
        }

        public void Dispose()
        {
            _enemiesRemainingHandler.WaveEnded -= OnWaveEnded;
            _playerService.Player.GetIsDead().Unsubscribe(OnPlayerIsDeadChanged);
            _baseService.Base.GetIsDead().Unsubscribe(OnBaseDefeated);
        }
    }
}
