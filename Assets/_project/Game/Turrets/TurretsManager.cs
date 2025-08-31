using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ObbyDefender
{
    public sealed class TurretsManager
    {
        [ShowInInspector] private readonly List<TurretInstaller> _activeTurrets = new();

        public List<TurretInstaller> ActiveTurrets => _activeTurrets;


        public bool TryAddTurret(TurretInstaller turret)
        {
            if (!_activeTurrets.Contains(turret))
            {
                _activeTurrets.Add(turret);
                return true;
            }

            return false;
        }

        public bool TryRemoveTurret(TurretInstaller turret)
        {
            if (_activeTurrets.Contains(turret))
            {
                _activeTurrets.Remove(turret);
                return true;
            }

            return false;
        }
    }
}
