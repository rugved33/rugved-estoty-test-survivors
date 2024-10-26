using System;

namespace SurvivorGame
{
    public class GameModel
    {
        private PlayerModel _playerModel;
        public event Action<int, int> OnPlayerHealthChanged;
        public event Action<int> OnEnemyKilled;
        public event Action OnPlayerDeath;

        public GameModel(PlayerModel player)
        {
            _playerModel = player;

            _playerModel.OnCurrentHealthChanged += (health, maxHealth) => OnPlayerHealthChanged?.Invoke(health, maxHealth);
            _playerModel.OnEnemyKilled += kills => OnEnemyKilled?.Invoke(kills);
            _playerModel.OnPlayerDead += () => OnPlayerDeath?.Invoke();
        }

        public int GetPlayerMaxHealth()
        {
            return _playerModel.MaxHealth;
        }
        public int GetPlayerHealth()
        {
            return _playerModel.CurrentHealth;
        }
    }
}
