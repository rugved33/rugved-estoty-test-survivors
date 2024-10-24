using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public class PlayerInstaller : MonoInstaller
    {

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInNewPrefab(_playerPrefab).AsSingle();
            Container.Bind<WeaponBase>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerModel>().AsSingle().WithArguments(_playerConfig.playerHealth, _playerConfig.playerAttackSpeed, _playerConfig.playerKillTarget);
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle().NonLazy();
        }
    }
}
