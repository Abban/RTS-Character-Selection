using Zenject;

namespace BBX.Controls
{
    public class ControlsInstaller : MonoInstaller
    {
        public SelectionDragGuiHandler.Components boxComponents;
        public SelectingHandler.Settings selectingSettings;
        public ControllingHandler.Settings controllingSettings;
        public CursorHandler.Settings cursorSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ControlsController>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<SelectionDragGuiHandler>()
                .AsSingle()
                .WithArguments(boxComponents);
            
            Container.Bind<SelectingHandler>()
                .AsSingle()
                .WithArguments(selectingSettings);
            
            Container.Bind<ControllingHandler>()
                .AsSingle()
                .WithArguments(controllingSettings);

            Container.BindInterfacesAndSelfTo<CursorHandler>()
                .AsSingle()
                .WithArguments(cursorSettings);
        }
    }
}