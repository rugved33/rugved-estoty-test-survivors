using System;
using UnityEngine;

namespace SurvivorGame
{
    public enum EnemyState
    {
        Chasing,
        Attacking,
        Dead,
        SIZE
    }
    public class EnemyPresenter : MonoBehaviour , IDamageable
    {
        private  EnemyModel _enemyModel;
        private  EnemyView _enemyView;
        private  IPlayer _player;
        private StateMachine _stateMachine;
        public event Action OnEnemyKilled;

        private MovementHandler _movementHandler;
        private AttackHandler _attackHandler;
        
        public void Initialize(EnemyModel enemyModel, EnemyView enemyView, IPlayer player)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _player= player;

            _movementHandler = new MovementHandler(_enemyView, _player);
            _attackHandler = new AttackHandler(_player, _enemyModel.Damage, _enemyModel.AttackInterval);

            _enemyModel.OnCurrentHealthChanged += CheckHealth;

            BootStates();
        }

        private void BootStates()
        {
            _stateMachine = new StateMachine((int)EnemyState.SIZE, false);
            _stateMachine.RegisterState(EnemyState.Chasing, null, OnChaseUpdate, null);
            _stateMachine.RegisterState(EnemyState.Attacking, null, OnAttackUpdate, null);
            _stateMachine.RegisterState(EnemyState.Dead, OnDeathEnter, null, null);
            _stateMachine.SetState(EnemyState.Chasing);
        }

        private void OnChaseUpdate()
        {
            _movementHandler.MoveTowardsPlayer(_enemyModel.MoveSpeed);

            if (_movementHandler.IsPlayerInRange(_enemyModel.AttackRange))
            {
                _stateMachine.SetState(EnemyState.Attacking);
            }
        }
        private void OnAttackUpdate()
        {
            _attackHandler.TryAttack();

            if (!_movementHandler.IsPlayerInRange(_enemyModel.AttackRange))
            {
                _stateMachine.SetState(EnemyState.Chasing);
            }
        }
        private void OnDeathEnter()
        {
            _enemyView.PlayDead();
            _player.EnemyDestroyed();
            OnEnemyKilled?.Invoke();
            GetComponent<BoxCollider2D>().enabled = false;
        }

        private void Update()
        {
            if(_player.IsDead)
            {
                return;
            }

            _stateMachine.Update();
        }

        public void TakeDamage(int damage)
        {
            _enemyModel.TakeDamage(damage);
            _enemyView.PlayHitEffect();
        }

        private void CheckHealth(int health)
        {
            if(health <= 0)
            {
                _stateMachine.SetState(EnemyState.Dead);
            }
        }
    }
}
