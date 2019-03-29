using Zenject;

namespace BBX.Cameras
{
    public class CameraInstaller : MonoInstaller
    {
        public CameraModel.Components components;
        
        public override void InstallBindings()
        {
            Container.Bind<CameraModel.Components>()
                .FromInstance(components)
                .AsSingle();

            Container.Bind<CameraModel>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<RaycastHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}