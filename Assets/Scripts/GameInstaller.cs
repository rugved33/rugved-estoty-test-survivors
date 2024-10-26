using UnityEngine;
using Zenject;

namespace SurvivorGame
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private HUDView _hudView;

        public override void InstallBindings()
        {
            Container.Bind<GameModel>().AsSingle();
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<HUDView>().FromInstance(_hudView).AsSingle();
            Container.BindInterfacesAndSelfTo<HUDPresenter>().AsSingle().NonLazy();
        }
    }
}
