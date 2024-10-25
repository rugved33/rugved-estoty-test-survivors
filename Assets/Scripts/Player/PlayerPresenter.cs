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
        private  PlayerModel _playerModel;
        private  PlayerView _playerView;
        private  WeaponBase _weapon;
        private float _attackTimer = 0;
        private GameStateService _gameStateService;
        public bool IsDead { get => _playerModel.IsDead; }

        public PlayerPresenter(Joystick joystick, PlayerModel playerModel, PlayerView playerView, WeaponBase weaponBase, GameStateService gameStateService)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _weapon = weaponBase;
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

        private void HandleAttack()
        {
            if (_playerModel.IsDead || _weapon == null)
            {
                return;
            }

            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _playerModel.AttackSpeed)
            {
                _weapon.Attack();
                _attackTimer = 0; 
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