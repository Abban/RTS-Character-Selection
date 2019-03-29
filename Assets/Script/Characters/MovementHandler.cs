using UnityEngine;

namespace BBX.Characters
{
    public class MovementHandler
    {
        private PlayerModel.Components _components;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="components"></param>
        public MovementHandler(PlayerModel.Components components)
        {
            _components = components;
        }

        
        /// <summary>
        /// Move the player
        /// </summary>
        /// <param name="position"></param>
        public void MoveTo(Vector3 position)
        {
            _components.agent.SetDestination(position);
        }

        
        /// <summary>
        /// Stop Moving
        /// </summary>
        public void Stop()
        {
            _components.agent.isStopped = true;
        }
    }
}