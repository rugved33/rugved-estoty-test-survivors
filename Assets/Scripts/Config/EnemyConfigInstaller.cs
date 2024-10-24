using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    [CreateAssetMenu(fileName = "EnemyConfigInstaller", menuName = "EnemyConfigInstaller")]
    public class EnemyConfigInstaller : ScriptableObjectInstaller<EnemyConfigInstaller>
    {
        public EnemyConfig[] enemyConfigs;  

        public override void InstallBindings()
        {
            Container.BindInstance(enemyConfigs).AsSingle();
        }
    }
}