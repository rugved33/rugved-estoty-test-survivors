using UnityEngine;

namespace SurvivorGame
{
    [CreateAssetMenu(fileName = "Enemy Spawner Config", menuName = "Enemy Spawner Config", order = 51)]
    public class EnemySpawnerConfig : ScriptableObject
    {

        [Header("Bounds")]
        public int maxX = 10;
        public int minX = -10;
        public int minY = -10;
        public int maxY = 10;

        [Header("Spawn Distance Constraints")]
        public float minSpawnDistance = 2f;
        public float maxSpawnDistance = 10f;

        public WaveConfig[] waves; 
    }
}