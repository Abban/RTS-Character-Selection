using UnityEngine;
using BBX.Input.Interfaces;

namespace BBX.Input
{
    public class InputState : IInputState
    {
        public float XAxis { get; set; }
        public float YAxis { get; set; }
        public bool Shift { get; set; }
        public bool Control { get; set; }
        public bool LeftMouseDown { get; set; }
        public bool LeftMouseDragging { get; set; }
        public bool LeftMouseUp { get; set; }
        public bool LeftMouseClicked { get; set; }
        public bool RightMouseDown { get; set; }
        public bool RightMouseHeld { get; set; }
        public bool RightMouseUp { get; set; }
        public bool RightMouseClicked { get; set; }
        public Vector2 PointerPosition { get; set; }

        public void Reset()
        {
            XAxis = 0;
            YAxis = 0;
            Shift = false;
            Control = false;
            LeftMouseDown = false;
            LeftMouseDragging = false;
            LeftMouseUp = false;
            LeftMouseClicked = false;
            RightMouseDown = false;
            RightMouseHeld = false;
            RightMouseUp = false;
            RightMouseClicked = false;
            PointerPosition = Vector2.zero;
        }
    }
}