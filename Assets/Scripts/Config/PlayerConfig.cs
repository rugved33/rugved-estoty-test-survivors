using UnityEngine;

namespace SurvivorGame
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig", order = 51)]
    public class PlayerConfig : ScriptableObject
    {
        public int playerHealth;
        public float playerAttackSpeed;
    }
}
