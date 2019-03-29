using UnityEngine;
using Zenject;

namespace BBX.Characters
{
    public class CharacterInstaller : MonoInstaller
    {
        private Vector3 _spawnPoint;
        private Transform _bulletParent;
        
        public PlayerModel.Components components;
        public SelectionHandler.Settings selectionSettings;


        [Inject]
        public void Construct(
            [InjectOptional] Vector3 spawnPoint,
            [InjectOptional] Transform bulletParent)
        {
            _spawnPoint = spawnPoint;
            _bulletParent = bulletParent;
        }
        
        public override void InstallBindings()
        {
            Container.BindInstance(_spawnPoint)
                .WhenInjectedInto<PlayerFacade>();
            
            Container.Bind<PlayerModel>()
                .AsSingle()
                .WithArguments(components);

            Container.Bind<SelectionHandler>()
                .AsSingle()
                .WithArguments(selectionSettings);
            
            Container.Bind<MovementHandler>()
                .AsSingle()
                .WithArguments(components);
        }
    }
}