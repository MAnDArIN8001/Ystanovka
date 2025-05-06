using Camera;
using Unity.Cinemachine;
using UnityEngine;

namespace UI
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _currentCamera;
        [SerializeField] private CinemachineCamera _rotatableCamera;

        [Space, SerializeField] private CameraRigger _cameraRigger;

        public void MoveToCamera(CinemachineCamera camera)
        {
            _currentCamera.gameObject.SetActive(false);
            _currentCamera = camera;
            _currentCamera.gameObject.SetActive(true);

            _cameraRigger.IsInCorrectPoint = _currentCamera == _rotatableCamera;
        }
    }
}