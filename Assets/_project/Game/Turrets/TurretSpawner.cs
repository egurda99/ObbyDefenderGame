using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObbyDefender
{
    public sealed class TurretSpawner : IDisposable
    {
        private readonly TurretBuilderSystem _turretBuilderSystem;
        private readonly Transform _turretZoneContainer;
        private readonly Transform _turretPrefab;
        private readonly TurretsManager _turretsManager;
        private readonly BulletFactory _bulletFactory;

        public event Action<Vector3> OnTurretSpawned;


        public TurretSpawner(TurretBuilderSystem turretBuilderSystem,
            Transform turretZoneContainer, Transform turretPrefab, TurretsManager turretsManager,
            BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            _turretsManager = turretsManager;
            _turretZoneContainer = turretZoneContainer;
            _turretPrefab = turretPrefab;
            _turretBuilderSystem = turretBuilderSystem;

            _turretBuilderSystem.OnTurretSpawnRequested += SpawnTurret;
        }

        private void SpawnTurret(Vector3 position)
        {
            var turretGO = Object.Instantiate(_turretPrefab, position, Quaternion.identity, _turretZoneContainer);

            if (!turretGO.TryGetComponent(out TurretInstaller turretInstaller))
                return;
            turretInstaller.SetBulletFactory(_bulletFactory);

            _turretsManager.TryAddTurret(turretInstaller);
            _turretBuilderSystem.OnTurretSpawned(position);

            OnTurretSpawned?.Invoke(position);
        }

        public void Dispose()
        {
            _turretBuilderSystem.OnTurretSpawnRequested -= SpawnTurret;
        }
    }
}
