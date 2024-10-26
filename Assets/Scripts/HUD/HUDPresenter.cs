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
            _gameModel.OnPlayerEnemyKilled += UpdateEnemyCount;
            _gameModel.OnPlayerDeath += OnPlayerDead;

            InitializeHealthBar();
        }

        private void InitializeHealthBar()
        {
            UpdateHealthBar(_gameModel.GetPlayerModel().CurrentHealth, _gameModel.GetPlayerModel().MaxHealth);
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

