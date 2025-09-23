using Atomic.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ObbyDefender.Scripts
{
    public sealed class LoadSceneTrigger : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneEntityProxy entity))
            {
                if (entity.HasPlayerTag())
                {
                    SceneManager.LoadScene(_sceneName);
                }
            }
        }
    }
}
