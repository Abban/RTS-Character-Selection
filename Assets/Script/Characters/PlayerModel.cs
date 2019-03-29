using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace BBX.Characters
{
    public class PlayerModel
    {
        private Components _components;
        
        public bool Selected { get; set; }
        public bool Selecting { get; set; }
        public Vector3 Center => _components.collider.bounds.center;
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="components"></param>
        public PlayerModel(
            Components components)
        {
            _components = components;
        }
        
        
        public Color SelectionColor
        {
            get { return _components.selectionMarker.color; }
            set { _components.selectionMarker.color = value; }
        }
        
        
        [Serializable]
        public class Components
        {
            public NavMeshAgent agent;
            public Collider collider;
            public Transform transform;
            public Image selectionMarker;
        }
    }
}