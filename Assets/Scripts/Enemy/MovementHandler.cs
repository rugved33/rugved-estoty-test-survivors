using UnityEngine;

namespace SurvivorGame
{
    public class MovementHandler
    {
        private readonly EnemyView _enemyView;
        private readonly IPlayer _player;

        public MovementHandler(EnemyView enemyView, IPlayer player)
        {
            _enemyView = enemyView;
            _player = player;
        }

        public void MoveTowardsPlayer(float speed)
        {
            Vector3 direction = (_player.GetPosition() - _enemyView.Position).normalized;
            _enemyView.Move(direction, speed);
        }

        public bool IsPlayerInRange(float range)
        {
            return Vector3.Distance(_enemyView.Position, _player.GetPosition()) <= range;
        }
    }
}
