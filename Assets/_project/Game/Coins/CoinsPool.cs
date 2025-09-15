using Atomic.Entities;
using Zenject;

namespace ObbyDefender
{
    public sealed class CoinsPool : MonoMemoryPool<CoinInstaller>
    {
        protected override void OnCreated(CoinInstaller item)
        {
            base.OnCreated(item);
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(CoinInstaller item)
        {
            item.gameObject.SetActive(true);

            var sceneEntity = item.GetComponent<SceneEntity>();
            var behaviour = sceneEntity.GetBehaviour<ReturnCoinToPoolBehaviour>();
            behaviour.SetPool(this);
        }

        protected override void OnDespawned(CoinInstaller item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
