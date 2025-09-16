using _UpgradePractice.Scripts;
using UnityEngine;

namespace ObbyDefender.DI
{
    public sealed class GameBootstrapHelper : MonoBehaviour
    {
        [SerializeField] private UpgradeCatalog _upgradeCatalog;

        public UpgradeCatalog UpgradeCatalog => _upgradeCatalog;
    }
}