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
        private const float BulletLifeTime = 1;

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
            transform.position += _direction * _speed * Time.deltaTime;
            DetectEnemiesInRange();
            CheckLifeTime();
        }

        private void CheckLifeTime()
        {
            _currentLifeTime += Time.deltaTime;
            if(_currentLifeTime > BulletLifeTime)
            {
                DestroyBullet();
            }
        }

        private void DetectEnemiesInRange()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, DamageRange, _enemyLayer);

            foreach (var hitCollider in hitEnemies)
            {
                var enemy = hitCollider.GetComponent<EnemyPresenter>();
                if (enemy != null)
                {
                    DealDamage(enemy);
                    break;  
                }
            }
        }

        private void DealDamage(EnemyPresenter enemy)
        {
            enemy.TakeDamage(_damage);
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
