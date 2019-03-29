using UnityEngine;

namespace BBX.Cameras
{
    public class RaycastHandler
    {
        private CameraModel _cameraModel;

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cameraModel"></param>
        public RaycastHandler(CameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }
        
        
        /// <summary>
        /// Fire a ray at a point and see whats there
        /// </summary>
        /// <param name="position"></param>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public RaycastHit FireRay(Vector3 position, LayerMask layerMask)
        {
            RaycastHit hit;

            Physics.Raycast(
                _cameraModel.Camera.ScreenPointToRay(position),
                out hit,
                _cameraModel.Camera.farClipPlane,
                layerMask
            );

            return hit;
        }
    }
}