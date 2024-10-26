using UnityEngine;

namespace SurvivorGame
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private bool _invertedSprite;
        private Animator _animator;  
        private EnemyAnimator _enemyAnimator; 
        public Vector3 Position => transform.position;

        private const float DestroyTime = 3f;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _enemyAnimator = new EnemyAnimator(_animator);
        }

        public void Move(Vector3 direction, float speed)
        {
            transform.position += speed * direction.normalized * Time.deltaTime;
            Flip(direction);
        }
        private void Flip(Vector3 direction)
        {
            bool shouldFaceRight = direction.x > 0 ? !_invertedSprite : _invertedSprite;

            transform.localScale = new Vector3(
                shouldFaceRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }

        public void PlayDead()
        {
            _enemyAnimator.PlayDead();
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            Destroy(gameObject, DestroyTime);
        }

        public void PlayHitEffect()
        {
            _enemyAnimator.PlayHit();
        }
    }
}
