using UnityEngine;

namespace SurvivorGame
{
    public class EnemyAnimator
    {
        private readonly Animator _animator;

        private static readonly int DeadHash = Animator.StringToHash("Dead");
        private static readonly int HitHash = Animator.StringToHash("Hit");

        public EnemyAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayDead()
        {
            if (_animator)
            {
                _animator.SetBool(DeadHash, true);
            }
        }

        public void PlayHit()
        {
            if (_animator)
            {
                _animator.SetTrigger(HitHash);
            }
        }
    }
}
