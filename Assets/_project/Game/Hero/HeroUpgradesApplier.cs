using ObbyDefender.Scripts;
using ShootEmUp;

namespace ObbyDefender.DI
{
    public sealed class HeroUpgradesApplier : IGameStartListener
    {
        private readonly PlayerService _playerService;
        private readonly HeroData _heroData;
        private readonly HeroInstaller _heroInstaller;

        public HeroUpgradesApplier(PlayerService playerService, HeroData heroData)
        {
            _playerService = playerService;
            _heroData = heroData;

            _heroInstaller = _playerService.Player.gameObject.GetComponent<HeroInstaller>();
        }

        public void OnStartGame()
        {
            _heroInstaller.ConfigurePlayer(_heroData);
        }
    }
}
