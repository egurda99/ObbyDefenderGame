using Atomic.Entities;
using UnityEngine;
using Zenject;

namespace ObbyDefender
{
    public class MeleeEnemyPool : MonoMemoryPool<MeleeBrainrotAnimalInstaller>
    {
        // Дополнительно можно добавлять OnSpawned / OnDespawned
        protected override void OnSpawned(MeleeBrainrotAnimalInstaller item)
        {
            base.OnSpawned(item);
            // item.OnSpawned();
            item.OnSpawned(this);

            var sceneEntity = item.GetComponent<SceneEntity>();
            var behaviour = sceneEntity.GetBehaviour<ReturnMeleeAnimalToPoolBehaviour>();
            behaviour.SetPool(this);
        }

        protected override void Reinitialize(MeleeBrainrotAnimalInstaller item)
        {
            item.OnRespawned();
        }

        protected override void OnDespawned(MeleeBrainrotAnimalInstaller item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
            item.gameObject.transform.position = Vector3.zero;
        }
    }
}
