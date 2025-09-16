using Atomic.Entities;
using Unity.Cinemachine;
using UnityEngine;

namespace ObbyDefender.DI
{
    public sealed class MetaSceneInstallerHelper : MonoBehaviour
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

        public Transform PistolBulletPrefab => _pistolBulletPrefab;

        public Transform M16BulletPrefab => _m16BulletPrefab;

        public Transform RangeAnimalBulletPrefab => _rangeAnimalBulletPrefab;

        public Transform BulletContainer => _bulletContainer;

        public SceneEntity PlayerPrefab => _playerPrefab;

        public Transform SpawnPoint => _spawnPoint;

        public Transform PlayerContainer => _playerContainer;

        public CinemachineCamera PlayerCamera => _playerCamera;
    }
}
