using System;
using UnityEngine;
using BBX.Characters;
using BBX.Cameras;

namespace BBX.Controls
{
    /// <summary>
    /// Handles player left clicks
    /// </summary>
    public class SelectingHandler
    {
        private Settings _settings;
        private CameraFacade _cameraFacade;
        private PlayerRegistry _playerRegistry;

        private Vector3 _startPosition;
        private Vector3 _currentPosition;
        private Bounds _selectionBounds = new Bounds();


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="cameraFacade"></param>
        /// <param name="playerRegistry"></param>
        public SelectingHandler(
            Settings settings,
            CameraFacade cameraFacade,
            PlayerRegistry playerRegistry)
        {
            _settings = settings;
            _cameraFacade = cameraFacade;
            _playerRegistry = playerRegistry;
        }


        /// <summary>
        /// When the player starts selecting
        /// </summary>
        /// <param name="position"></param>
        public void OnStartSelect(Vector3 position)
        {
            _startPosition = position;
        }


        /// <summary>
        /// When the player is dragging a box
        /// </summary>
        /// <param name="position"></param>
        public void OnSelecting(Vector3 position)
        {
            _currentPosition = position;
            SetBounds();
            CheckIntersections();
        }


        /// <summary>
        /// Set up some screen bounds of the selected area
        /// </summary>
        private void SetBounds()
        {
            var v1 = _cameraFacade.Camera.ScreenToViewportPoint(_startPosition);
            var v2 = _cameraFacade.Camera.ScreenToViewportPoint(_currentPosition);

            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);

            min.z = _cameraFacade.Camera.nearClipPlane;
            max.z = _cameraFacade.Camera.farClipPlane;

            _selectionBounds.SetMinMax(min, max);
        }


        /// <summary>
        /// Check what selectables are intersecting the bounds
        /// </summary>
        private void CheckIntersections()
        {
            foreach (var character in _playerRegistry.Players)
            {
                var characterInBounds = _selectionBounds.Contains(
                    _cameraFacade.Camera.WorldToViewportPoint(character.Center)
                );

                if (characterInBounds && !character.IsSelecting)
                {
                    character.StartedSelecting();
                }
                else if (!characterInBounds && character.IsSelecting)
                {
                    character.StoppedSelecting();
                }
            }
        }


        /// <summary>
        /// When the player has finished dragging
        ///
        /// If normal select then remove non selecting players
        /// If its a shift select then add selecting players to selection
        /// If deselect or shift-deselect then just remove selecting
        /// </summary>
        public void OnSelected(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.Select:
                    GroupSelect();
                    break;
                case SelectionType.ControlSelect:
                    GroupControlSelect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
        /// <summary>
        /// Handle a select action
        /// </summary>
        private void GroupSelect()
        {
            foreach (var player in _playerRegistry.NotSelecting)
            {
                player.Deselect();
            }

            GroupControlSelect();
        }

        
        /// <summary>
        /// Handle a shift select action
        /// </summary>
        private void GroupControlSelect()
        {
            foreach (var player in _playerRegistry.Selecting)
            {
                player.Select();
            }
        }
        
        
        /// <summary>
        /// Handle a left click event
        /// </summary>
        /// <param name="position"></param>
        /// <param name="selectionType"></param>
        public void OnLeftClick(Vector3 position, SelectionType selectionType)
        {
            var hit = _cameraFacade.FireRay(position, _settings.playerMask);
            
            if (hit.transform != null)
            {
                Select(
                    hit.transform.gameObject.GetComponent<PlayerFacade>(),
                    selectionType
                );
            }
            else
            {
                Deselect();
            }
        }
        
        
        /// <summary>
        /// Do stuff when a player character is clicked
        /// </summary>
        /// <param name="player"></param>
        /// <param name="selectionType"></param>
        private void Select(PlayerFacade player, SelectionType selectionType)
        {   
            if (player == null) return;

            switch (selectionType)
            {
                case SelectionType.Select:
                    ReplaceSelected(player);
                    break;
                case SelectionType.ControlSelect:
                    player.Select();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectionType), selectionType, null);
            }
        }

        
        /// <summary>
        /// Deselect all players
        /// </summary>
        private void Deselect()
        {
            foreach (var character in _playerRegistry.Selected)
            {
                character.Deselect();
            }
        }
        
        
        /// <summary>
        /// Set the passed character as selected and deselect all others
        /// </summary>
        /// <param name="selected"></param>
        private void ReplaceSelected(PlayerFacade selected)
        {
            if (!selected.IsSelected) selected.Select();

            // Loop all selected and set the ones that aren't the passed to deselected
            foreach (var character in _playerRegistry.Selected)
            {
                if (character != selected)
                {
                    character.Deselect();
                }
            }
        }
        
        
        [Serializable]
        public class Settings
        {
            public LayerMask playerMask;
        }
    }
}