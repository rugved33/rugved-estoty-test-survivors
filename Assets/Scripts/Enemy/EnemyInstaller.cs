using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfigInstaller _enemyConfigInstaller; 
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig; 
        public override void InstallBindings()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().WithArguments(_enemyConfigInstaller.enemyConfigs);
            Container.Bind<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();
        }
    }
}
