using System;

namespace SurvivorGame
{
    public class PlayerModel
    {
        public int CurrentHealth { get; private set; }
        public int EnemiesDestroyed { get; private set; }
        public int MaxHealth { get; private set; }
        public int KillTarget { get; private set; }
        public bool IsDead { get; private set; }
        public float AttackSpeed { get; private set; }

        public event Action<int,int> OnCurrentHealthChanged;
        public event Action<int,int> OnEnemyKilled;
        public event Action OnPlayerDead;

        public PlayerModel(int maxHealth, float attackSpeed, int killTarget)
        {
            CurrentHealth = maxHealth;
            MaxHealth = maxHealth;
            AttackSpeed = attackSpeed;
            KillTarget = killTarget;
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

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            OnCurrentHealthChanged?.Invoke(CurrentHealth, MaxHealth);
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void AddEnemyDestroyed()
        {
            EnemiesDestroyed += 1;
            OnEnemyKilled?.Invoke(EnemiesDestroyed, KillTarget);
        }
    }
}