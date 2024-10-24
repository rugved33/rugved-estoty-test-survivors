using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public class EnemySpawner : ITickable
    {
        private EnemySpawnerConfig _config;
        private IPlayer _player;
        private IEnemyFactory _enemyFactory;
        private float _timeSinceLastSpawn = 0f;
        private int _currentWaveIndex = 0;
        private int _enemiesSpawnedInWave = 0;
        private WaveConfig _currentWave;

        [Inject]
        public void Construct(IPlayer player, IEnemyFactory enemyFactory, EnemySpawnerConfig enemySpawnerConfig)
        {
            _player = player;
            _enemyFactory = enemyFactory;
            _config = enemySpawnerConfig;

            StartNextWave();
        }

        void ITickable.Tick()
        {
            if (_currentWave == null || _player.IsDead)
            {
                return;
            }

            if (CanSpawn(Time.deltaTime) && _enemiesSpawnedInWave  < _currentWave.numberOfEnemies)
            {
                SpawnEnemy();
            }

            if (_enemiesSpawnedInWave >= _currentWave.numberOfEnemies)
            {
                StartNextWave();
            }
        }

        private void SpawnEnemy()
        {
            var enemyTypes = _currentWave.enemyTypes;
            var randomType = enemyTypes[Random.Range(0, enemyTypes.Length)];

            var spawnPoint = GetValidSpawnPosition();
            _enemyFactory.CreateEnemy(randomType, spawnPoint, _player);
            _enemiesSpawnedInWave++;
        } 
        private void StartNextWave()
        {
            if (_currentWaveIndex < _config.waves.Length)
            {
                _currentWave = _config.waves[_currentWaveIndex];
                _currentWaveIndex++;
                _timeSinceLastSpawn = 0;
                _enemiesSpawnedInWave = 0;
                Debug.Log($"Wave {_currentWaveIndex} started.");
            }
            else
            {
                _currentWave = null;
                Debug.Log("All waves completed!");
            }
        }

        private bool CanSpawn(float deltaTime)
        {
            _timeSinceLastSpawn += deltaTime;

            if (_timeSinceLastSpawn >= _currentWave.spawnInterval)
            {
                _timeSinceLastSpawn = 0;
                return true;
            }

            return false;
        }
        
        private Vector2 GetValidSpawnPosition()
        {
            Vector2 playerPosition = new Vector2(_player.GetPosition().x, _player.GetPosition().y); 
            Vector2 spawnPosition;

            do
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float distance = Random.Range(_config.minSpawnDistance, _config.maxSpawnDistance);
                spawnPosition = playerPosition + randomDirection * distance;
                spawnPosition = ClampPositionWithinBounds(spawnPosition);
            }
            while (!IsWithinBounds(spawnPosition)); 

            return spawnPosition;
        }

        private Vector2 ClampPositionWithinBounds(Vector2 position)
        {
            float clampedX = Mathf.Clamp(position.x, _config.minX, _config.maxX);
            float clampedY = Mathf.Clamp(position.y, _config.minY, _config.maxY);
            return new Vector2(clampedX, clampedY);
        }

        private bool IsWithinBounds(Vector2 position)
        {
            return position.x >= _config.minX && position.x <= _config.maxX && position.y >= _config.minY && position.y <= _config.maxY;
        }
    }
}
