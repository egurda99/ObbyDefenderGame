using System;
using ObbyDefender.Scripts;
using ShootEmUp;

namespace ObbyDefender.DI
{
    public sealed class HeroUpgradesApplier : IGameStartListener, IDisposable
    {
        private readonly PlayerService _playerService;
        private readonly HeroData _heroData;
        private readonly HeroInstaller _heroInstaller;

        public HeroUpgradesApplier(PlayerService playerService, HeroData heroData)
        {
            _playerService = playerService;
            _heroData = heroData;

            _heroData.HealthChanged += OnStatChanged;
            _heroData.SpeedChanged += OnStatChanged;

            _heroInstaller = _playerService.Player.gameObject.GetComponent<HeroInstaller>();
        }

        public void OnStartGame()
        {
            ApplyStats();
        }

        private void OnStatChanged()
        {
            ApplyStats();
        }

        private void ApplyStats()
        {
            _heroInstaller.ConfigurePlayer(_heroData);
        }


        public void Dispose()
        {
            _heroData.HealthChanged -= OnStatChanged;
            _heroData.SpeedChanged -= OnStatChanged;
        }
    }
}
