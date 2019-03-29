using System;
using System.Linq;
using UnityEngine;
using BBX.Cameras;
using BBX.Characters;

namespace BBX.Controls
{
    /// <summary>
    /// Handles player right clicks
    /// </summary>
    public class ControllingHandler
    {
        private Settings _settings;
        private CameraFacade _camera;
        private PlayerRegistry _playerRegistry;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="camera"></param>
        /// <param name="playerRegistry"></param>
        public ControllingHandler(
            Settings settings,
            CameraFacade camera,
            PlayerRegistry playerRegistry)
        {
            _settings = settings;
            _camera = camera;
            _playerRegistry = playerRegistry;
        }

        
        /// <summary>
        /// Handle a right click event
        /// </summary>
        /// <param name="position"></param>
        public void OnRightClick(Vector3 position)
        {
            var hit = _camera.FireRay(position, _settings.groundMask);
            
            if (hit.transform != null)
            {
                HandleMovementClick(hit.point);
            }
        }

        
        /// <summary>
        /// Handle a movement click
        /// </summary>
        /// <param name="position"></param>
        private void HandleMovementClick(Vector3 position)
        {
            var selected = _playerRegistry.Selected.ToArray();

            for (var i = 0; i < selected.Length; i++)
            {
                selected[i].MoveTo(position);
            }
        }

        
        [Serializable]
        public class Settings
        {
            public LayerMask groundMask;
        }
    }
}