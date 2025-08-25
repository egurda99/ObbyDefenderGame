using Atomic.Entities;
using Elementary;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ObbyDefender.DI
{
    public sealed class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        [SerializeField] private SceneInstallerHelper _sceneInstallerHelper;
        public override void InstallBindings()
        {
            BindPlayer();
            BindInput();
        }



        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<SceneEntity>(_sceneInstallerHelper.PlayerPrefab, _sceneInstallerHelper.SpawnPoint.position,
                Quaternion.identity, _sceneInstallerHelper.PlayerContainer);

            Container.Bind<PlayerService>().AsSingle().WithArguments(player);

            _sceneInstallerHelper.PlayerCamera.Follow = player.transform;

            var sensor  = player.GetComponentInChildren<ColliderDetectionOverlapSphere>();

            Container.BindInterfacesTo<NearestTargetObserver>().AsSingle().WithArguments(sensor,player);


        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<HeroInputController>().AsSingle();
        }
    }
}
