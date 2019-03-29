using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace BBX.Controls
{
    /// <summary>
    /// This handles drawing a GUI Selection Box on the screen when the player drags the mouse
    /// </summary>
    public class SelectionDragGuiHandler : IInitializable
    {
        private Components _components;

        private Vector3 _startPosition;
        private Vector3 _currentPosition;
        private Vector2 _scaleFactor;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="components"></param>
        public SelectionDragGuiHandler(
            Components components)
        {
            _components = components;
        }


        /// <summary>
        /// Initialize this object
        /// </summary>
        public void Initialize()
        {
            _components.image.gameObject.SetActive(false);
            SetCanvasScaleFactor();
        }


        /// <summary>
        /// Set up box drawing
        /// </summary>
        /// <param name="position"></param>
        public void StartBox(Vector3 position)
        {
            SetCanvasScaleFactor();
            _startPosition = position;
        }

        
        /// <summary>
        /// Do box drawing
        /// </summary>
        /// <param name="position"></param>
        public void DrawBox(Vector3 position)
        {
            _components.image.gameObject.SetActive(true);
            _currentPosition = position;
            SetSelectorPosition();
            SetSelectorSize();
        }

        
        /// <summary>
        /// Hide the box
        /// </summary>
        public void HideBox()
        {
            _startPosition = Vector2.zero;
            _currentPosition = Vector2.zero;
            _components.image.gameObject.SetActive(false);
        }


        /// <summary>
        /// Set the selector box's position in the GUI
        /// We're gonna set it to the mid point between the start and current position
        /// </summary>
        private void SetSelectorPosition()
        {
            var centerPos = (_startPosition + _currentPosition) / 2f;

            // Get the world point relative to the canvas' camera
            Vector3 position;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _components.canvasTransform,
                centerPos,
                _components.canvas.worldCamera,
                out position
            );

            // Set the position
            _components.image.rectTransform.position = position;
        }

        
        /// <summary>
        /// Set the selector box's size in the GUI
        /// </summary>
        private void SetSelectorSize()
        {
            // Calculate the size
            var size = new Vector2(
                Mathf.Abs(_startPosition.x - _currentPosition.x),
                Mathf.Abs(_startPosition.y - _currentPosition.y)
            );
            
            // Scale size to canvas and set it
            _components.image.rectTransform.sizeDelta = size * _scaleFactor;
        }


        /// <summary>
        /// Set the scale factor for drawing the box
        /// </summary>
        private void SetCanvasScaleFactor()
        {
            var canvasRect = _components.canvasTransform.rect;
            
            _scaleFactor = new Vector2(
                canvasRect.width / Screen.width,
                canvasRect.height / Screen.height
            );
        }


        [Serializable]
        public class Components
        {
            public Image image;
            public Canvas canvas;
            public RectTransform canvasTransform;
        }
    }
}