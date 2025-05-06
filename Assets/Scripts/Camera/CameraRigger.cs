using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Camera
{
    public class CameraRigger : MonoBehaviour
    {
        public bool IsInCorrectPoint = true;
        
        [Header("Target")]
        [SerializeField] private Transform _target;

        [FormerlySerializedAs("rotationSpeed")]
        [Header("Rotation Settings")]
        [SerializeField] private float _rotationSpeed = 1.5f; 
        [SerializeField] private Vector2 _verticalClamp = new Vector2(-45f, 45f);
        [SerializeField] private Vector2 _horizontalClamp = new Vector2(-90f, 90f);
        
        [Header("Zoom Settings")]
        [SerializeField] private float _zoomSpeed = 5f;
        [SerializeField] private float _minDistance = 2f;
        [SerializeField] private float _maxDistance = 10f;
        
        [Header("References")]
        [SerializeField] private Transform _cameraTransform;

        private Vector3 _currentRotation;
        private float _currentDistance;

        private void Start()
        {
            _currentDistance = Vector3.Distance(transform.position, _cameraTransform.position);
            Vector3 angles = transform.eulerAngles;
            _currentRotation = new Vector2(angles.y, angles.x);
        }

        private void Update()
        {
            if (Mouse.current.rightButton.isPressed && IsInCorrectPoint)    
            {
                Vector2 mouseDelta = Mouse.current.delta.ReadValue() * _rotationSpeed * Time.deltaTime;
    
                _currentRotation.x += mouseDelta.x;
                _currentRotation.z -= mouseDelta.y;

                _currentRotation.x = Mathf.Clamp(_currentRotation.x, _horizontalClamp.x, _horizontalClamp.y);
                _currentRotation.z = Mathf.Clamp(_currentRotation.z, _verticalClamp.x, _verticalClamp.y);

                transform.rotation = Quaternion.Euler(0, _currentRotation.x, _currentRotation.z);
            }

            float scroll = Mouse.current.scroll.ReadValue().y;
            _currentDistance -= scroll * _zoomSpeed * Time.deltaTime;
            _currentDistance = Mathf.Clamp(_currentDistance, _minDistance, _maxDistance);

            _cameraTransform.localPosition = new Vector3(_currentDistance, 2f, 0);
        }
    }
}
