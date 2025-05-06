using System;
using DG.Tweening;
using UnityEngine;

namespace Ystanovka.Controls
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private bool _isZ;
        [SerializeField] private bool _isAlternate;
        
        [SerializeField] private float duration = 0.5f;
        
        [Space, SerializeField] private Ease easeType = Ease.Linear;

        [Space, SerializeField] private Vector3 _rotationValue;

        private Tween _rotationTween;

        private void OnDestroy()
        {
            if (_rotationTween is not null && _rotationTween.IsActive())
            {
                _rotationTween.Kill();
            }
        }

        /// <summary>
        /// Вращает объект вдоль оси Z в зависимости от переданного значения.
        /// </summary>
        /// <param name="value">0, 1 или 2</param>
        public void RotateToValue(int value)
        {
            if (_rotationTween is not null && _rotationTween.IsActive())
            {
                _rotationTween.Kill();
            }
            
            float targetAngle = _isAlternate ? Alternate(value) : GetAngleFromValue(value);
    
            if (_isZ)
            {
                _rotationValue.z = targetAngle;   
            }
            else
            {
                _rotationValue.x = targetAngle;
            }

            _rotationTween = transform.DORotate(_rotationValue, duration)
                .SetEase(easeType);
        }

        /// <summary>
        /// Определяет угол вращения на основе переданного значения.
        /// </summary>
        private float GetAngleFromValue(int value)
        {
            switch (value)
            {
                case 2: return 45f;
                case 1: return 90f;
                case 0: return 135f;
                default:
                    Debug.LogWarning($"Некорректное значение: {value}. Ожидалось 0, 1 или 2.");
                    return 0f;
            }
        }

        private float Alternate(int value)
        {
            switch (value)
            {
                case 2: return 45f;
                case 1: return 0f;
                case 0: return -45f;
                default:
                    Debug.LogWarning($"Некорректное значение: {value}. Ожидалось 0, 1 или 2.");
                    return 0f;
            }
        }
    }
}