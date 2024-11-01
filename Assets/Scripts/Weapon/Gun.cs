using UnityEngine;

namespace SurvivorGame
{
    public class Gun : WeaponBase
    {
        [SerializeField] private GunConfig _config;     
        private Pool _bulletPool;
        private const int BulletPoolSize = 10;

        public void Start()
        {
            _bulletPool = new Pool(true, _config.bulletPrefab.GetComponent<Bullet>(), BulletPoolSize, transform);
        }

        public override void Attack()
        {
            Transform enemyTransform = GetNearestEnemy();

            if (enemyTransform != null)
            {
                ShootAt(enemyTransform);
            }
        }
        private Transform GetNearestEnemy()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _config.attackRange, _config.enemyLayer);
            Transform nearestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (var enemyCollider in hitEnemies)
            {
                float distance = Vector2.Distance(transform.position, enemyCollider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemyCollider.transform;
                }
            }

            return nearestEnemy;
        }

        public void ShootAt(Transform enemyTransform)
        {
            Vector3 directionToEnemy = (enemyTransform.position - transform.position).normalized;
            SpawnBullet(directionToEnemy);
        }

        private void SpawnBullet(Vector3 direction)
        {
            var bullet = _bulletPool.GetElement<Bullet>();
            bullet.transform.position = transform.position;
            bullet.gameObject.SetActive(true);
            bullet.transform.parent = null;
            bullet.Initialize(direction, _config.bulletSpeed, _config.damage, _config.enemyLayer);
            bullet.OnDestroyedCallback += OnBulletDestroyed;
        }

        private void OnBulletDestroyed(Bullet bullet)
        {
            bullet.Reset();
            bullet.transform.parent = transform;
            bullet.OnDestroyedCallback -= OnBulletDestroyed;
            bullet.gameObject.SetActive(false);
            _bulletPool.ReturnElement(bullet);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _config.attackRange);
        }
    }
}
