using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private HUDView _hudView;
        [SerializeField] private EnemyConfigInstaller _enemyConfigInstaller; 
        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig; 
        public override void InstallBindings()
        {
            Container.Bind<GameModel>().AsSingle();
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<HUDView>().FromInstance(_hudView).AsSingle();
            Container.BindInterfacesAndSelfTo<HUDPresenter>().AsSingle().NonLazy();
            
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle().WithArguments(_enemyConfigInstaller.enemyConfigs);
            Container.Bind<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();
        }
    }
}
