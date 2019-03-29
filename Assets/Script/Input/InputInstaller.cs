using Zenject;
using BBX.Input.Interfaces;

namespace BBX.Input
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        public InputSettings inputSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<IInputState>()
                .To<InputState>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<DesktopHandler>()
                .AsSingle()
                .WithArguments(inputSettings)
                .NonLazy();
        }
    }
}