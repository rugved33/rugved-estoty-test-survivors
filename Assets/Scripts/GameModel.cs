using System;

namespace SurvivorGame
{
    public class GameModel
    {
        private PlayerModel _playerModel;
        public event Action<int, int> OnPlayerHealthChanged;
        public event Action<int> OnPlayerEnemyKilled;
        public event Action OnPlayerDeath;

        public GameModel(PlayerModel player)
        {
            _playerModel = player;

            _playerModel.OnCurrentHealthChanged += (health, maxHealth) => OnPlayerHealthChanged?.Invoke(health, maxHealth);
            _playerModel.OnEnemyKilled += kills => OnPlayerEnemyKilled?.Invoke(kills);
            _playerModel.OnPlayerDead += () => OnPlayerDeath?.Invoke();
        }

        public PlayerModel GetPlayerModel()
        {
            return _playerModel;
        }
    }
}
