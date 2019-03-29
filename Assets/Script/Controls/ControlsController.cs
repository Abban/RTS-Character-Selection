using System;
using Zenject;
using BBX.Input.Interfaces;

namespace BBX.Controls
{
    public class ControlsController : IInitializable, ILateTickable
    {
        private IInputState _input;
        private SelectionDragGuiHandler _selectionBox;
        private SelectingHandler _selectingHandler;
        private ControllingHandler _controllingHandler;
        private CursorHandler _cursorHandler;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        private enum States
        {
            Controlling,
            Selecting
        }

        private States _state;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input"></param>
        /// <param name="selectionBox"></param>
        /// <param name="selectingHandler"></param>
        /// <param name="controllingHandler"></param>
        /// <param name="cursorHandler"></param>
        public ControlsController(
            IInputState input,
            SelectionDragGuiHandler selectionBox,
            SelectingHandler selectingHandler,
            ControllingHandler controllingHandler,
            CursorHandler cursorHandler)
        {
            _input = input;
            _selectionBox = selectionBox;
            _selectingHandler = selectingHandler;
            _controllingHandler = controllingHandler;
            _cursorHandler = cursorHandler;
        }


        /// <summary>
        /// Default the state
        /// </summary>
        public void Initialize()
        {
            _state = States.Controlling;
        }


        /// <summary>
        /// Runs late once a frame, handles basic state machine
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void LateTick()
        {
            switch (_state)
            {
                case States.Controlling:
                    Controlling();
                    break;
                case States.Selecting:
                    Selecting();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        /// <summary>
        /// Nothing is being selected
        /// </summary>
        private void Controlling()
        {
            _cursorHandler.OnControlling(_input.PointerPosition);
            
            if (_input.LeftMouseDown)
            {
                _state = States.Selecting;

                _selectionBox.StartBox(_input.PointerPosition);
                _selectingHandler.OnStartSelect(_input.PointerPosition);
            }
            else if (_input.RightMouseClicked)
            {
                _controllingHandler.OnRightClick(_input.PointerPosition);
            }
        }


        /// <summary>
        /// Player is selecting 
        /// </summary>
        private void Selecting()
        {
            var selectionType = _input.Control ? SelectionType.ControlSelect : SelectionType.Select;
            
            if (_input.LeftMouseDragging)
            {
                _cursorHandler.OnSelecting();
                _selectionBox.DrawBox(_input.PointerPosition);
                _selectingHandler.OnSelecting(_input.PointerPosition);
            }

            if (_input.LeftMouseClicked)
            {
                _selectingHandler.OnLeftClick(_input.PointerPosition, selectionType);
            }
            // Only call selected if there was a drag
            else if (_input.LeftMouseUp)
            {
                _selectingHandler.OnSelected(selectionType);
            }

            if (_input.LeftMouseUp)
            {
                _state = States.Controlling;
                _selectionBox.HideBox();
            }
        }
    }
}