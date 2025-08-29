using UnityEngine;

namespace ObbyDefender
{
    [CreateAssetMenu(
        fileName = "TurretsConfig",
        menuName = "SO/New TurretsConfig"
    )]
    public sealed class TurretsConfig : ScriptableObject
    {
        [SerializeField] private TurretInfoHolder[] _turretsInfoHolder;

        public TurretInfoHolder[] TurretsInfoHolder => _turretsInfoHolder;
    }
}