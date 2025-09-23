using System.Collections.Generic;
using _UpgradePractice.Scripts;
using MyCodeBase;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ObbyDefender.Scripts
{
    public sealed class TurretUpgradesPopup : Popup
    {
        [SerializeField] private UpgradeView _viewPrefab;

        [SerializeField] private Transform _container;


        private UpgradeCatalog _upgradeCatalog;

        private readonly List<ViewHolder> _viewHolders = new();

        private UpgradesManager _upgradesManager;
        private readonly List<UpgradeConfig> _heroUpgrades = new();

        [Inject]
        public void Construct(UpgradesManager upgradesManager)
        {
            _upgradesManager = upgradesManager;
            _upgradeCatalog = upgradesManager.UpgradeCatalog;

            InitConfigs();
        }


        protected override void OnShow()
        {
            base.OnShow();
            Show();
        }

        protected override void OnHide()
        {
            base.OnHide();
            Hide();
        }


        [Button]
        public void Show()
        {
            for (int i = 0, count = _heroUpgrades.Count; i < count; i++)
            {
                var config = _heroUpgrades[i];
                ShowUpgrade(config);
            }
        }

        private void InitConfigs()
        {
            var allUpgrades = _upgradeCatalog.GetAllUpgrades();

            _heroUpgrades.Clear();
            for (var index = 0; index < allUpgrades.Length; index++)
            {
                var upgradeConfig = allUpgrades[index];
                if (upgradeConfig.Type == UpgradeType.TURRET)
                {
                    _heroUpgrades.Add(upgradeConfig);
                }
            }
        }

        [Button]
        public void Hide()
        {
            for (int i = 0, count = _viewHolders.Count; i < count; i++)
            {
                var vh = _viewHolders[i];
                HideUpgrade(vh);
            }

            _viewHolders.Clear();
        }

        private void ShowUpgrade(UpgradeConfig config)
        {
            var view = Instantiate(_viewPrefab, _container);
            var presenter = new TurretUpgradesPresenter(config, view, _upgradesManager);
            presenter.Start();

            _viewHolders.Add(new ViewHolder(view, presenter));
        }

        private void HideUpgrade(ViewHolder vh)
        {
            vh.Presenter.Stop();
            Destroy(vh.View.gameObject);
        }

        private readonly struct ViewHolder
        {
            public readonly UpgradeView View;
            public readonly TurretUpgradesPresenter Presenter;

            public ViewHolder(UpgradeView view, TurretUpgradesPresenter presenter)
            {
                View = view;
                Presenter = presenter;
            }
        }
    }
}
