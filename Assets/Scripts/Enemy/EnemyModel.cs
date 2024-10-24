using System;
using UnityEngine;

namespace SurvivorGame
{
    public class EnemyModel
    {
        public float AttackRange { get; private set; }
        public float AttackInterval { get; private set; }
        public float MoveSpeed { get; private set; }
        public int Damage { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }
        public event Action<int> OnCurrentHealthChanged;

        public EnemyModel(int health, float attackRange, float attackInterval, int damage, float moveSpeed)
        {
            AttackRange = attackRange;
            AttackInterval = attackInterval;
            MoveSpeed = moveSpeed;
            Damage = damage;
            CurrentHealth = health;
            IsDead = false;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            OnCurrentHealthChanged?.Invoke(CurrentHealth);

            if(CurrentHealth <= 0)
            {
                IsDead = true;
            }
        }
    }

    [System.Serializable]
    public class EnemyConfig
    {
        public EnemyType enemyType;
        public int health;
        public float attackRange;
        public float attackInterval;
        public float moveSpeed;
        public int damage;
        public GameObject enemyPrefab;
    }
}
