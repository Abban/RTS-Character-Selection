using UnityEngine;
using Zenject;

namespace BBX.Cameras
{
    public class CameraFacade : MonoBehaviour
    {
        private CameraModel _camera;
        private RaycastHandler _rayHandler;

        [Inject]
        public void Construct(
            CameraModel cameraModel,
            RaycastHandler rayHandler)
        {
            _camera = cameraModel;
            _rayHandler = rayHandler;
        }


        public Camera Camera => _camera.Camera;
        public RaycastHit FireRay(Vector3 position, LayerMask layerMask) => _rayHandler.FireRay(position, layerMask);
    }
}