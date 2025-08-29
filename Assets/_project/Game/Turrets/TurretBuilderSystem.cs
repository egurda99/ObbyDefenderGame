using System;
using System.Collections.Generic;
using MyCodeBase;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ObbyDefender
{
    public sealed class TurretBuilderSystem : IDisposable
    {
        private readonly TurretsConfig _turretsConfig;
        private readonly TurretBuildZoneView _turretBuildZoneView;
        private readonly MoneyStorage _moneyStorage;

        private readonly List<BuildZoneHolder> _entries = new();
        private readonly Transform _turretZoneContainer;

        public event Action<Vector3> OnTurretSpawnRequested;


        public TurretBuilderSystem(TurretsConfig turretsConfig, TurretBuildZoneView turretBuildZoneView,
            MoneyStorage moneyStorage, Transform turretZoneContainer)
        {
            _turretZoneContainer = turretZoneContainer;
            _turretsConfig = turretsConfig;
            _turretBuildZoneView = turretBuildZoneView;
            _moneyStorage = moneyStorage;
            Initialize();
        }

        public void Initialize()
        {
            foreach (var turretInfo in _turretsConfig.TurretsInfoHolder)
            {
                var view = Object.Instantiate(_turretBuildZoneView, turretInfo.Position.position, Quaternion.identity,
                    _turretZoneContainer);

                var adapter = new TurretBuildZoneAdapter(
                    turretInfo.PriceToBuild,
                    turretInfo.TimeToBuild,
                    view,
                    _moneyStorage);

                var holder = new BuildZoneHolder(adapter, view, turretInfo.Position);
                _entries.Add(holder);

                adapter.OnZoneBuildRequested += OnZoneBuildRequested;
                adapter.Start();
            }
        }

        private void OnZoneBuildRequested(Vector3 position)
        {
            OnTurretSpawnRequested?.Invoke(position);
        }

        public void Dispose()
        {
            foreach (var entry in _entries)
            {
                entry.Adapter.OnZoneBuildRequested -= OnZoneBuildRequested;
                entry.Adapter.Stop();
            }

            _entries.Clear();
        }

        private sealed class BuildZoneHolder
        {
            public TurretBuildZoneAdapter Adapter { get; }
            public TurretBuildZoneView View { get; }
            public Transform Position { get; }

            public BuildZoneHolder(TurretBuildZoneAdapter adapter, TurretBuildZoneView view, Transform position)
            {
                Adapter = adapter;
                View = view;
                Position = position;
            }
        }
    }
}
