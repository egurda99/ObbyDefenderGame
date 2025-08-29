using System;
using UnityEngine;

namespace ObbyDefender
{
    public sealed class TurretSpawner : IDisposable
    {
        private readonly TurretsConfig _turretsConfig;
        private readonly TurretBuilderSystem _turretBuilderSystem;

        public TurretSpawner(TurretsConfig turretsConfig, TurretBuilderSystem turretBuilderSystem)
        {
            _turretsConfig = turretsConfig;
            _turretBuilderSystem = turretBuilderSystem;

            _turretBuilderSystem.OnTurretSpawnRequested += SpawnTurret;
        }

        private void SpawnTurret(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _turretBuilderSystem.OnTurretSpawnRequested += SpawnTurret;
        }
    }
}