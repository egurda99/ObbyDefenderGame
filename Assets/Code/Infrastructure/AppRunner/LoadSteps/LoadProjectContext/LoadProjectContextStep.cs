using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.AppRunner
{
    [CreateAssetMenu(fileName = "LoadProjectContextStep", menuName = "AppRunner/LoadProjectContextStep")]
    public sealed class LoadProjectContextStep : LoadingStepScriptableObject
    {
        [SerializeField] private ProjectContext _projectContext;
        [SerializeField] private string _title = "Loading DI container";
        [SerializeField] private float _waitTime = 0.1f;

        public override string Title => _title;

        public override async UniTask Do()
        {

            await UniTask.WaitUntil(() => ProjectContext.HasInstance);

            await UniTask.Delay((int)(_waitTime * 1000));
        }
    }
}
