using UnityEngine;

namespace SurvivorGame
{
    public class AttackHandler
    {
        private readonly IPlayer _player;
        private readonly int _damage;
        private readonly float _attackInterval;
        private float _attackTimer;

        public AttackHandler(IPlayer player, int damage, float attackInterval)
        {
            _player = player;
            _damage = damage;
            _attackInterval = attackInterval;
            _attackTimer = 0;
        }

        public void TryAttack()
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackInterval)
            {
                _player.ApplyDamage(_damage);
                _attackTimer = 0;
            }
        }
    }
}
