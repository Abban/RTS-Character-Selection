using System;
using BBX.Cameras;
using UnityEngine;
using Zenject;

namespace BBX.Controls
{
    public class CursorHandler : IInitializable
    {
        private Settings _settings;
        private CameraFacade _camera;

        private enum CursorState
        {
            Move,
            Attack
        }

        private CursorState _state;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="camera"></param>
        public CursorHandler(
            Settings settings,
            CameraFacade camera)
        {
            _settings = settings;
            _camera = camera;
        }
        
        
        /// <summary>
        /// When this object is initialised
        /// </summary>
        public void Initialize()
        {
            _state = CursorState.Move;
            SetCursor(_settings.moveTexture, _settings.pointerOffset);
        }
        
        
        /// <summary>
        /// Handle the cursor types when the player is controlling
        /// </summary>
        public void OnControlling(Vector2 position)
        {
            var hit = _camera.FireRay(position, _settings.masks);

            switch (_state)
            {
                case CursorState.Move:
                    HandleMove(hit);
                    break;
                case CursorState.Attack:
                    HandleAttack(hit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
        /// <summary>
        /// Force the cursor back to move when player is dragging
        /// </summary>
        public void OnSelecting()
        {
            if (_state != CursorState.Attack) return;
            
            _state = CursorState.Move;
            SetCursor(_settings.moveTexture, _settings.pointerOffset);
        }

        
        /// <summary>
        /// Handle the move state
        /// </summary>
        /// <param name="hit"></param>
        private void HandleMove(RaycastHit hit)
        {
            if (hit.transform == null || !hit.transform.CompareTag("Enemy")) return;
            
            _state = CursorState.Attack;
            SetCursor(_settings.attackTexture, _settings.targetOffset);
        }

        
        /// <summary>
        /// Handle the attack state
        /// </summary>
        /// <param name="hit"></param>
        private void HandleAttack(RaycastHit hit)
        {
            if (hit.transform != null && hit.transform.CompareTag("Enemy")) return;
            
            _state = CursorState.Move;
            SetCursor(_settings.moveTexture, _settings.pointerOffset);
        }


        /// <summary>
        /// Set a cursor texture
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="offset"></param>
        private void SetCursor(Texture2D texture, Vector2 offset)
        {
            Cursor.SetCursor(
                texture,
                offset,
                _settings.cursorMode
            );
        }


        [Serializable]
        public class Settings
        {
            public CursorMode cursorMode = CursorMode.Auto;
            public Texture2D moveTexture;
            public Texture2D attackTexture;
            public LayerMask masks;
            public Vector2 pointerOffset;
            public Vector2 targetOffset;
        }
    }
}