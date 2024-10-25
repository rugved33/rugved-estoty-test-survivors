using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SurvivorGame
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField, Range(float.Epsilon, 5f)]
        private float _speed;

        [SerializeField] private Slider _healthBar;
        [SerializeField] private SpriteRenderer _spriteRenderer; 

        private Animator _animator;
        private Color _originalColor;
        private Vector3 _originalScale;

        private const float BounceScaleFactor = 1.2f;
        private const float BounceDuration = 0.2f;
        private const float TweenDuration = 0.1f;

        private static readonly int DeadHash = Animator.StringToHash("Dead");
        private static readonly int RunHash = Animator.StringToHash("Run");

        private void Start()
        {
            _animator = GetComponent<Animator>();

            if (_spriteRenderer != null)
            {
                _originalColor = _spriteRenderer.color;
            }
            _originalScale = transform.localScale;
        }

        public void Move(Vector2 direction)
        {
            var oldPosition = transform.position;
            transform.position = Vector3.Lerp(oldPosition, oldPosition + (Vector3)direction * _speed, Time.deltaTime);
            PlayRun();
            Flip(direction);
        }

        private void Flip(Vector3 direction)
        {
            var shouldFaceRight = direction.x > 0;

            transform.localScale = new Vector3(
                shouldFaceRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        public void PlayRun()
        {
            _animator.SetBool(RunHash,true);
        }

        public void PlayIdle()
        {
            _animator.SetBool(RunHash,false);
        }

        public Vector2 Position
        {
            get => transform.position;
        }

        public void PlayHitEffect()
        {
            if (_spriteRenderer == null) return;

            PlayColorEffect();
            PlayBounceEffect();
        }

        private void PlayColorEffect()
        {
            _spriteRenderer.DOColor(Color.red, TweenDuration)
                        .OnComplete(() => _spriteRenderer.DOColor(_originalColor, TweenDuration));
        }

        private void PlayBounceEffect()
        {
            transform.DOKill(); 
            transform.localScale = _originalScale;

            transform.DOScale(_originalScale * BounceScaleFactor, BounceDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => transform.DOScale(_originalScale, BounceDuration).SetEase(Ease.InQuad));
        }

        public void OnPlayerDeath()
        {
            _animator.SetBool(DeadHash,true);
        }
    }
}