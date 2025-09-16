using _UpgradePractice.Scripts;
using MyCodeBase;
using ObbyDefender.Scripts;
using UnityEngine;
using Zenject;

namespace ObbyDefender.DI
{
    public sealed class GameBootstrap : MonoInstaller<GameBootstrap>
    {
        [SerializeField] private GameBootstrapHelper _helper;

        public override void InstallBindings()
        {
            BindInput();
            BindMoneyStorage();
            BindHeroData();
            BindTurretData();
            BindUpgradeManager();
        }

        private void BindHeroData()
        {
            var heroData = new HeroData(1f, 1f);
            Container.Bind<HeroData>().FromInstance(heroData).AsSingle();
        }

        private void BindTurretData()
        {
            var turretData = new TurretData(1f, 1f);
            Container.Bind<TurretData>().FromInstance(turretData).AsSingle();
        }

        private void BindMoneyStorage()
        {
            Container.Bind<MoneyStorage>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<KeyboardInput>().AsSingle();
        }

        private void BindUpgradeManager()
        {
            Container.Bind<UpgradesManager>().AsSingle().WithArguments(_helper.UpgradeCatalog);
        }
    }
}
