using System;
using BBX.Characters;
using Zenject;

namespace BBX.Main
{
    public class LevelController : IInitializable
    {
        private Components _components;
        private PlayerRegistry _playerRegistry;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="components"></param>
        /// <param name="playerRegistry"></param>
        public LevelController(
            Components components,
            PlayerRegistry playerRegistry)
        {
            _components = components;
            _playerRegistry = playerRegistry;
        }
        
        
        /// <summary>
        /// When the scene starts add all the players to the registry
        /// </summary>
        public void Initialize()
        {
            foreach (var player in _components.players)
            {
                _playerRegistry.AddPlayer(player);
            }
        }
        
        
        [Serializable]
        public class Components
        {
            public PlayerFacade[] players;
        }
    }
}