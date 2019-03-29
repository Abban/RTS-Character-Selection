using System;
using UnityEngine;

namespace BBX.Cameras
{
    public class CameraModel
    {
        private Components _components;
        private Vector3 _midpoint;

        public CameraModel(Components components)
        {
            _components = components;
        }

        /// <summary>
        /// The camera component
        /// </summary>
        public Camera Camera => _components.camera;
        
        
        /// <summary>
        /// The current camera position
        /// </summary>
        public Vector3 Position
        {
            get { return _components.transform.position; }
            set { _components.transform.position = value; }
        }


        public Vector3 Midpoint
        {
            get { return _midpoint; }
            set { _midpoint = value; }
        }


        /// <summary>
        /// The current camera rotation
        /// </summary>
        public Vector3 Rotation
        {
            get { return _components.transform.eulerAngles; }
            set { _components.transform.eulerAngles = value; }
        }


        public float AspectRatio => Screen.width / Screen.height;
        

        [Serializable]
        public class Components
        {
            public Camera camera;
            public Transform transform;
        }
    }
}