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
            if (DetectEnemiesInRange())
            {
                ShootBullet();
            }
        }

        private bool DetectEnemiesInRange()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _config.attackRange, _config.enemyLayer);
            return hitEnemies.Length > 0;
        }

        public void ShootBullet()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _config.attackRange, _config.enemyLayer);

            if (hitEnemies.Length == 0)
            {
                return;
            }

            Transform enemyTransform = hitEnemies[0].transform;
            Vector3 directionToEnemy = (enemyTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            SpawnBullet(directionToEnemy);
        }

        private void SpawnBullet(Vector3 direction)
        {
            var bullet = _bulletPool.GetElement<Bullet>();
            bullet.transform.position = transform.position;
            bullet.gameObject.SetActive(true);
            bullet.transform.parent = null;
            bullet.Initialize(direction, _config.bulletSpeed, _config.damage, _config.enemyLayer);
            bullet.OnDestroyedCallback += OnBulletDestoryed;
        }

        private void OnBulletDestoryed(Bullet bullet)
        {
            bullet.transform.parent = transform;
            bullet.OnDestroyedCallback -= OnBulletDestoryed;
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
