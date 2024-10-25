using UnityEngine;
using System;

namespace SurvivorGame
{
    public class Bullet: MonoBehaviour
    {      
        private int _damage;
        private float _speed; 
        private Vector3 _direction;
        private LayerMask _enemyLayer;
        public event Action<Bullet> OnDestroyedCallback;
        private float _currentLifeTime;
        private const float DamageRange = 0.5f;
        private const float BulletLifeTime = 2;

        public void Initialize(Vector3 direction,
                                float speed,
                                int damage,
                                LayerMask enemyLayer)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
            _enemyLayer = enemyLayer;
            _currentLifeTime = 0;
        }

        private void Update()
        {
            MoveBullet();
            DetectAndDamage();
            CheckLifeTime();
        }
        private void MoveBullet()
        {
            transform.position += _direction * _speed * Time.deltaTime;
        }

        private void CheckLifeTime()
        {
            _currentLifeTime += Time.deltaTime;
            if(_currentLifeTime > BulletLifeTime)
            {
                DestroyBullet();
            }
        }

        private void DetectAndDamage()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, DamageRange, _enemyLayer);

            foreach (var hitCollider in hitEnemies)
            {
                if (hitCollider.TryGetComponent(out IDamageable damageable))
                {
                    DealDamage(damageable);
                    break;
                }
            }
        }

        private void DealDamage(IDamageable target)
        {
            target.TakeDamage(_damage);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            OnDestroyedCallback?.Invoke(this); 
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, DamageRange);
        }
    }
}
