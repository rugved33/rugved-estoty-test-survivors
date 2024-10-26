namespace SurvivorGame
{
    public class HUDPresenter
    {
        private readonly GameModel _gameModel;
        private readonly HUDView _hudView;

        public HUDPresenter(GameModel gameModel, HUDView hudView)
        {
            _gameModel = gameModel;
            _hudView = hudView;

            _gameModel.OnPlayerHealthChanged += UpdateHealthBar;
            _gameModel.OnEnemyKilled += UpdateEnemyCount;
            _gameModel.OnPlayerDeath += OnPlayerDead;

            InitializeHUD();
        }

        private void InitializeHUD()
        {
            var playerHealth = _gameModel.GetPlayerHealth();
            var playerMaxHealth = _gameModel.GetPlayerMaxHealth();
            _hudView.UpdateHealthBar(playerHealth, playerMaxHealth);
        }

        public void UpdateHealthBar(int health, int maxHealth)
        {
            _hudView.UpdateHealthBar(health, maxHealth);
        }

        private void UpdateEnemyCount(int currentKill)
        {
            _hudView.UpdateEnemiesDestroyed(currentKill);
        }

        private void OnPlayerDead()
        {
            _hudView.HideJoystick();
        }
    }
}

