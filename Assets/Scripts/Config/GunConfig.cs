using UnityEngine;

namespace SurvivorGame
{
    [CreateAssetMenu(fileName = "GunConfig", menuName = "GunConfig", order = 51)]
    public class GunConfig : ScriptableObject
    {
        public GameObject bulletPrefab;
        public float attackRange = 5f;
        public int damage = 2;
        public float bulletSpeed = 5f;
        public LayerMask enemyLayer;
    }
}