using System;

namespace SurvivorGame
{
    public class GameStateService
    {
        public bool IsPlayerDead { get; private set; }

        public event Action OnPlayerDead;

        public void SetPlayerDead()
        {
            IsPlayerDead = true;
            OnPlayerDead?.Invoke();
        }
    }
}
