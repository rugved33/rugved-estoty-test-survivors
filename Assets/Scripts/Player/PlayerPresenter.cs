using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public interface IPlayer
    {
        bool IsDead { get; }
        Vector3 GetPosition();
        void EnemyDestroyed();
        void ApplyDamage(int damage);
    }
    public class PlayerPresenter :  IPlayer, ITickable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly IWeapon _weapon;
        private readonly GameStateService _gameStateService;
        private float _timeSinceLastAttack = 0;
        public bool IsDead { get => _playerModel.IsDead; }

        public PlayerPresenter(Joystick joystick, PlayerModel playerModel, PlayerView playerView, IWeapon weapon, GameStateService gameStateService)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _weapon = weapon;
            _gameStateService = gameStateService;
            _playerModel.OnPlayerDead += OnPlayerDeath;

            joystick.OnInput += HandleMovement;
            joystick.OnInputReleased += _playerView.PlayIdle;
        }
        public Vector3 GetPosition()
        {
            return _playerView.Position;
        }

        private void HandleMovement(Vector2 direction)
        {
            _playerView.Move(direction);
        }

        public void Tick()
        {
            HandleAttack();
        }

        private bool CanAttack()
        {
            return _timeSinceLastAttack >= _playerModel.AttackSpeed;
        }

        private void HandleAttack()
        {
            if (_playerModel.IsDead || _weapon == null)
            {
                return;
            }

            _timeSinceLastAttack += Time.deltaTime;

            if (CanAttack())
            {
                _weapon.Attack();
                _timeSinceLastAttack = 0; 
            }
        }

        public void ApplyDamage(int damage)
        {
            if (!_playerModel.IsDead)
            {
                _playerModel.ApplyDamage(damage);
                _playerView.PlayHitEffect();
            }
        }

        public void EnemyDestroyed()
        {
            _playerModel.AddEnemyDestroyed();
        }

        private void OnPlayerDeath()
        {
            _playerView.OnPlayerDeath();
            _gameStateService.SetPlayerDead();
        }
    }
}