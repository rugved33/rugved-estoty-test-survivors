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
    public class EnemyPresenter : MonoBehaviour
    {
        private  EnemyModel _enemyModel;
        private  EnemyView _enemyView;
        private  IPlayer _player;
        private float _attackTimer = 0;
        private StateMachine _stateMachine;
        public event Action OnEnemyKilled;
        private string state = "";
        
        public void Initialize(EnemyModel enemyModel, EnemyView enemyView, IPlayer player)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _player= player;
            
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
            MoveTowardsPlayer();
            CheckPlayerInRange();
        }
        private void OnAttackUpdate()
        {
            AttackPlayer();
            CheckPlayerInRange();
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
            state = _stateMachine.GetState().ToString();
        }

        private void MoveTowardsPlayer()
        {
            var playerPos = _player.GetPosition();
            Vector3 direction = (playerPos - _enemyView.Position).normalized;
            _enemyView.Move(direction, _enemyModel.MoveSpeed);
        }

        private void CheckPlayerInRange()
        {
            float distanceToPlayer = Vector3.Distance(_enemyView.Position, _player.GetPosition());
            bool isPlayerInRange = distanceToPlayer <= _enemyModel.AttackRange;

            if(isPlayerInRange)
            {
                _stateMachine.SetState(EnemyState.Attacking);
            }
            else
            {
                _stateMachine.SetState(EnemyState.Chasing);
            }
        }

        private void AttackPlayer()
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > _enemyModel.AttackInterval)
            {
                _player.ApplyDamage(_enemyModel.Damage);
                _attackTimer = 0;
            }
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
