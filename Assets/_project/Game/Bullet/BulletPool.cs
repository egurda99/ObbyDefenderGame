using Atomic.Entities;
using Zenject;

namespace ObbyDefender
{
    public sealed class BulletPool : MonoMemoryPool<BulletInstaller>
    {
        protected override void OnCreated(BulletInstaller item)
        {
            base.OnCreated(item);
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(BulletInstaller item)
        {
            item.gameObject.SetActive(true);
            item.SetPool(this);

            var sceneEntity = item.GetComponent<SceneEntity>();
            var behaviour = sceneEntity.GetBehaviour<DestroyBulletByLifeTimeBehaviour>();
            behaviour.SetPool(this);
        }

        protected override void OnDespawned(BulletInstaller item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
