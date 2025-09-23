using UnityEngine;

namespace MyCodeBase.Utils
{
    public sealed class RandomAnimationSelector : StateMachineBehaviour
    {
        [SerializeField] private int _animationVariants = 3;
        [SerializeField] private string _parameterName;


        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var index = Random.Range(0, _animationVariants);
            animator.SetFloat($"{_parameterName}", index);
        }
    }
}
