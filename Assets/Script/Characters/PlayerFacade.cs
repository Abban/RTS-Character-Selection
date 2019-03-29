using UnityEngine;
using Zenject;

namespace BBX.Characters
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private SelectionHandler _selectionHandler;
        private MovementHandler _movementHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerModel"></param>
        /// <param name="selectionHandler"></param>
        /// <param name="movementHandler"></param>
        [Inject]
        public void Construct(
            PlayerModel playerModel,
            SelectionHandler selectionHandler,
            MovementHandler movementHandler)
        {
            _playerModel = playerModel;
            _selectionHandler = selectionHandler;
            _movementHandler = movementHandler;
        }
        
        public bool IsSelecting => _playerModel.Selecting;
        
        public bool IsSelected => _playerModel.Selected;
        
        public Vector3 Center => _playerModel.Center;
        
        public void StartedSelecting() => _selectionHandler.StartedSelecting();

        public void StoppedSelecting() => _selectionHandler.StoppedSelecting();

        public void Select() => _selectionHandler.Select();
        
        public void Deselect() => _selectionHandler.Deselect();

        public void MoveTo(Vector3 position) => _movementHandler.MoveTo(position);

        public void Stop() => _movementHandler.Stop();
    }
}