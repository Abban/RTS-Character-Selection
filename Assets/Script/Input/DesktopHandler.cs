using UnityEngine;
using Zenject;
using BBX.Input.Interfaces;

namespace BBX.Input
{
    public class DesktopHandler : ITickable
    {
        private Vector2 _leftStartPosition = Vector2.zero;
        private float _rightDownTime;
        private float _leftDistance;

        private InputSettings _settings;
        private readonly IInputState _inputState;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inputState"></param>
        public DesktopHandler(
            InputSettings settings,
            IInputState inputState)
        {
            _settings = settings;
            _inputState = inputState;
        }


        /// <summary>
        /// Look for mouse input on every frame
        /// Don't change the order of the methods
        /// </summary>
        public void Tick()
        {
            _inputState.Reset();

            CheckAxes();
            CheckButtons();
            SetMousePosition();

            CheckLeftMouseDown();
            CheckLeftMouseUp();
            CheckLeftMouseHold();

            CheckRightMouseDown();
            CheckRightMouseUp();
            CheckRightMouseHold();
        }


        /// <summary>
        /// Looks for directional input
        /// </summary>
        private void CheckAxes()
        {
            _inputState.XAxis = UnityEngine.Input.GetAxis("Horizontal");
            _inputState.YAxis = UnityEngine.Input.GetAxis("Vertical");
        }


        /// <summary>
        /// Look for button input
        /// </summary>
        private void CheckButtons()
        {
            _inputState.Shift = UnityEngine.Input.GetButtonDown("Shift") ||
                                UnityEngine.Input.GetButton("Shift");
            
            _inputState.Control = UnityEngine.Input.GetButtonDown("Control") ||
                                  UnityEngine.Input.GetButton("Control");
        }


        /// <summary>
        /// Store the current mouse position and distances from the start positions
        /// </summary>
        private void SetMousePosition()
        {
            _inputState.PointerPosition = UnityEngine.Input.mousePosition;
            _leftDistance = Vector2.Distance(_leftStartPosition, _inputState.PointerPosition);
        }


        /// <summary>
        /// Check left mouse down
        /// </summary>
        private void CheckLeftMouseDown()
        {
            if (!UnityEngine.Input.GetMouseButtonDown(0)) return;

            _leftStartPosition = _inputState.PointerPosition;
            _inputState.LeftMouseDown = true;
        }


        /// <summary>
        /// Check right mouse down
        /// </summary>
        private void CheckRightMouseDown()
        {
            if (!UnityEngine.Input.GetMouseButtonDown(1)) return;

            _rightDownTime = Time.time;
            _inputState.RightMouseDown = true;
        }


        /// <summary>
        /// Check a left mouse up
        /// </summary>
        private void CheckLeftMouseUp()
        {
            if (!UnityEngine.Input.GetMouseButtonUp(0)) return;

            _inputState.LeftMouseUp = true;

            // If distance is less than min then it's a click
            if (_leftDistance > _settings.dragDistanceThreshold) return;

            _inputState.LeftMouseClicked = true;
        }


        /// <summary>
        /// Check a right mouse up
        /// </summary>
        private void CheckRightMouseUp()
        {
            if (!UnityEngine.Input.GetMouseButtonUp(1)) return;

            _inputState.RightMouseUp = true;
            
            // If time is less than the threshold it's a click
            if (Time.time - _rightDownTime > _settings.holdTimeThreshold) return;

            _inputState.RightMouseClicked = true;
        }


        /// <summary>
        /// Check for left mouse held
        /// </summary>
        private void CheckLeftMouseHold()
        {
            if (!UnityEngine.Input.GetMouseButton(0)) return;

            // If distance is greater than min then it's a drag 
            if (_leftDistance <= _settings.dragDistanceThreshold) return;
            
            _inputState.LeftMouseDragging = true;
        }


        /// <summary>
        /// Check for right mouse held
        /// </summary>
        private void CheckRightMouseHold()
        {
            if (!UnityEngine.Input.GetMouseButton(1)) return;
            
            // If time held is greater than the threshold
            if (Time.time - _rightDownTime <= _settings.holdTimeThreshold) return;

            _inputState.RightMouseHeld = true;
        }
    }
}