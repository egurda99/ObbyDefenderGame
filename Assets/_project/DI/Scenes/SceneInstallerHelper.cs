using Atomic.Entities;
using Unity.Cinemachine;
using UnityEngine;

namespace ObbyDefender.DI
{
    public sealed class SceneInstallerHelper : MonoBehaviour
    {
        [Space] [Header("Player")] [SerializeField]
        private SceneEntity _playerPrefab;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _playerContainer;
        [SerializeField] private CinemachineCamera _playerCamera;

        [Space] [Header("Bullets")] [SerializeField]
        private Transform _pistolBulletPrefab;

        [SerializeField] private Transform _m16BulletPrefab;
        [SerializeField] private Transform _rangeAnimalBulletPrefab;

        [SerializeField] private Transform _bulletContainer;

        [Space] [Header("Turrets")] [SerializeField]
        private TurretsConfig _turretsConfig;

        [SerializeField] private Transform _turretPrefab;


        [SerializeField] private TurretBuildZoneView _turretBuildZoneView;
        [SerializeField] private Transform _turretZoneContainer;

        [Space] [Header("Base")] [SerializeField]
        private Transform _basePrefab;

        [SerializeField] private Transform _baseSpawnPoint;
        [SerializeField] private Transform _baseContainer;


        public Transform BasePrefab => _basePrefab;

        public Transform BaseSpawnPoint => _baseSpawnPoint;

        public Transform BaseContainer => _baseContainer;

        public Transform RangeAnimalBulletPrefab => _rangeAnimalBulletPrefab;

        public Transform TurretPrefab => _turretPrefab;

        public TurretsConfig TurretsConfig => _turretsConfig;

        public TurretBuildZoneView TurretBuildZoneView => _turretBuildZoneView;

        public Transform TurretZoneContainer => _turretZoneContainer;

        public Transform M16BulletPrefab => _m16BulletPrefab;

        public Transform PistolBulletPrefab => _pistolBulletPrefab;

        public Transform BulletContainer => _bulletContainer;

        public SceneEntity PlayerPrefab => _playerPrefab;

        public Transform SpawnPoint => _spawnPoint;

        public Transform PlayerContainer => _playerContainer;

        public CinemachineCamera PlayerCamera => _playerCamera;
    }
}
