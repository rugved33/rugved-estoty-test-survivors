using UnityEngine;

namespace SurvivorGame
{
    public class PlayerAnimator
    {
        private readonly Animator _animator;
        private static readonly int DeadHash = Animator.StringToHash("Dead");
        private static readonly int RunHash = Animator.StringToHash("Run");

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayRun()
        {
            _animator.SetBool(RunHash, true);
        }

        public void PlayIdle()
        {
            _animator.SetBool(RunHash, false);
        }

        public void PlayDeath()
        {
            _animator.SetBool(DeadHash, true);
        }
    }
}
