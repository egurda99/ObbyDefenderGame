using UnityEngine;

namespace _UpgradePractice.Scripts
{
    public abstract class Upgrade
    {
        public string Id => _config.Id;
        public int Level => _level;
        public int MaxLevel => _config.MaxLevel;
        public int NextPrice => _config.GetNextPrice(Level + 1);

        private readonly UpgradeConfig _config;
        private int _level;

        public bool IsMaxLevel => Level == MaxLevel;


        protected Upgrade(UpgradeConfig config)
        {
            _config = config;
            _level = 1;
        }

        public void SetLevel(int level)
        {
            _level = level;
            OnUpgrade();
        }


        public void LevelUp()
        {
            if (IsMaxLevel)
            {
                Debug.Log("Is max level!");
                return;
            }

            _level++;

            OnUpgrade();
        }

        protected abstract void OnUpgrade();
    }
}
