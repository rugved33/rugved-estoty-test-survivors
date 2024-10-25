namespace SurvivorGame
{
    public class HUDPresenter
    {
        private readonly PlayerModel _playerModel;
        private readonly HUDView _hudView;

        public HUDPresenter(PlayerModel playerModel, HUDView hudView)
        {
            _playerModel = playerModel;
            _hudView = hudView;

            _playerModel.OnCurrentHealthChanged += UpdateHealthBar;
            _playerModel.OnEnemyKilled += UpdateEnemyCount;
            _playerModel.OnPlayerDead += OnPlayerDead;
            UpdateHealthBar(_playerModel.CurrentHealth, _playerModel.MaxHealth);
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

