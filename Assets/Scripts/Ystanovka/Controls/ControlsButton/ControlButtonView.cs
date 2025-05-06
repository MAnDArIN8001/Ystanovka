using System;
using DG.Tweening;
using UnityEngine;

namespace Ystanovka.Controls.ControlsButton
{
    public class ControlButtonView : MonoBehaviour
    {
        [SerializeField] private float _pressTime;

        [Space, SerializeField] private Ease _pressEase;

        [Space, SerializeField] private Vector3 _downFloatingValue;
        private Vector3 _defaultPosition;

        private ControlButton _controlButton;

        private Tween _pressingTween;

        private void Awake()
        {
            _controlButton = GetComponent<ControlButton>();
            _defaultPosition = transform.position;
        }

        private void OnEnable()
        {
            if (_controlButton is not null)
            {
                _controlButton.OnButtonPressed += HandleClick;
            }
        }

        private void OnDisable()
        {
            if (_controlButton is not null)
            {
                _controlButton.OnButtonPressed -= HandleClick;
            }
        }

        private void HandleClick()
        {
            if (_pressingTween is not null && _pressingTween.IsActive())
            {
                _pressingTween.Kill();
            }

            _pressingTween = transform.DOMoveY(_defaultPosition.y - _downFloatingValue.y, _pressTime)
                .SetEase(_pressEase)
                .OnComplete(() => _pressingTween = transform.DOMoveY(_defaultPosition.y, _pressTime).SetEase(_pressEase));
        }
    }
}