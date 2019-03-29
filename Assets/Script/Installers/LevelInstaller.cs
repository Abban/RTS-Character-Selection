using Zenject;
using BBX.Cameras;
using BBX.Characters;
using BBX.Main;

namespace BBX.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        public LevelController.Components levelComponents;
        public CameraFacade cameraFacade;

        public override void InstallBindings()
        {
            Container.Bind<PlayerRegistry>()
                .AsSingle();

            Container.BindInterfacesTo<LevelController>()
                .AsSingle()
                .WithArguments(levelComponents)
                .NonLazy();

            Container.Bind<CameraFacade>()
                .FromInstance(cameraFacade)
                .AsSingle();
        }
    }
}