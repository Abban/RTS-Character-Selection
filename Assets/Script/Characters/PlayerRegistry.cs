using System.Collections.Generic;
using System.Linq;

namespace BBX.Characters
{
    public class PlayerRegistry
    {
        private readonly List<PlayerFacade> _players = new List<PlayerFacade>();
        public IEnumerable<PlayerFacade> Players => _players;
        public IEnumerable<PlayerFacade> Selecting => _players.Where(x => x.IsSelecting).ToList();
        public IEnumerable<PlayerFacade> NotSelecting => _players.Where(x => !x.IsSelecting).ToList();
        public IEnumerable<PlayerFacade> Selected => _players.Where(x => x.IsSelected).ToList();
        
        
        /// <summary>
        /// When a player is instantiated
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayer(PlayerFacade player)
        {
            _players.Add(player);
        }
    }
}