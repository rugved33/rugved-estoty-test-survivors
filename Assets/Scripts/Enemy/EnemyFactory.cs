using UnityEngine;


namespace SurvivorGame
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly EnemyConfig[] _enemyConfigs;

        public EnemyFactory(EnemyConfig[] enemyConfigs)
        {
            _enemyConfigs = enemyConfigs;
        }

        public EnemyPresenter CreateEnemy(EnemyType enemyType, Vector3 spawnPosition, IPlayer player)
        {
            EnemyModel enemyModel;
            EnemyConfig config = GetEnemyConfig(enemyType);

            GameObject enemyObject = Object.Instantiate(config.enemyPrefab, spawnPosition, Quaternion.identity);
            enemyModel = new EnemyModel(config.health, config.attackRange, config.attackInterval, config.damage, config.moveSpeed);

            var enemyView = enemyObject.GetComponent<EnemyView>();
            var enemyPresenter = enemyObject.AddComponent<EnemyPresenter>();
            enemyPresenter.Initialize(enemyModel, enemyView, player);

            return enemyPresenter;
        }

        private EnemyConfig GetEnemyConfig(EnemyType enemyType)
        {
            foreach (var config in _enemyConfigs)
            {
                if (config.enemyType == enemyType)
                {
                    return config;
                }
            }
            throw new System.Exception($"No config found for enemy type {enemyType}");
        }
    }
}
