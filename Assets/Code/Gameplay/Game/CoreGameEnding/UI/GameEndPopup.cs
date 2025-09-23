using System.Collections.Generic;
using _UpgradePractice.Scripts;
using MyCodeBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ObbyDefender.Scripts
{
    public sealed class GameEndPopup : Popup
    {
        [SerializeField] private EndGameView _view;

        private UpgradesManager _upgradesManager;
        private readonly List<UpgradeConfig> _heroUpgrades = new();
        private int _enemiesKilled;
        private int _waveBonus;
        private EndGameViewPresenter _presenter;
        private int _collectedMoney;


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

        public void Init(int enemiesKilled, int waveBonus, int collectedMoney)
        {
            _collectedMoney = collectedMoney;
            _waveBonus = waveBonus;
            _enemiesKilled = enemiesKilled;
        }


        [Button]
        public void Show()
        {
            _presenter = new EndGameViewPresenter(_view, _enemiesKilled, _waveBonus, _collectedMoney);
            _presenter.Start();
        }


        [Button]
        public void Hide()
        {
            _presenter.Stop();
        }
    }
}
