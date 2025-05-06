using Unity.Cinemachine;
using UnityEngine;

namespace UI
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _currentCamera;

        public void MoveToCamera(CinemachineCamera camera)
        {
            _currentCamera.gameObject.SetActive(false);
            _currentCamera = camera;
            _currentCamera.gameObject.SetActive(true);
        }
    }
}