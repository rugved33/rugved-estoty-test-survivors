using System;

namespace SurvivorGame
{
    public class PlayerModel
    {
        public int CurrentHealth { get; private set; }
        public int EnemiesDestroyed { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsDead { get; private set; }
        public float AttackSpeed { get; private set; }

        public event Action<int,int> OnCurrentHealthChanged;
        public event Action<int> OnEnemyKilled;
        public event Action OnPlayerDead;

        public PlayerModel(int maxHealth, float attackSpeed)
        {
            CurrentHealth = maxHealth;
            MaxHealth = maxHealth;
            AttackSpeed = attackSpeed;
        }

        public void ApplyDamage(int damage)
        {
            CurrentHealth -= damage;
            OnCurrentHealthChanged?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                IsDead = true;
                OnPlayerDead?.Invoke();
            }
        }

        public void AddEnemyDestroyed()
        {
            EnemiesDestroyed += 1;
            OnEnemyKilled?.Invoke(EnemiesDestroyed);
        }
    }
}