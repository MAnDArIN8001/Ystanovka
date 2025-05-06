using System;
using UnityEngine;
using Ystanovka.Controls;
using Ystanovka.Controls.ControlsButton;

namespace Ystanovka.Systems
{
    public class ValueSystem : AbstractSystem<float>, IStatable
    {
        public override event Action<float> OnValueChanged;

        private float _currentValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        [Space, SerializeField] private float _deltaPerClick;
        
        [Space, SerializeField] private ControlButton _moreControlsButton;
        [SerializeField] private ControlButton _lessControlsButton;

        public bool State { get; private set; }

        public float CurrentValue => _currentValue;

        private void Start()
        {
            _currentValue = _minValue;
        }

        public void Enable()
        {
            if (_moreControlsButton is not null)
            {
                _moreControlsButton.OnButtonPressed += HandleValueIncrease;
            }
            
            if (_lessControlsButton is not null)
            {
                _lessControlsButton.OnButtonPressed += HandleValueDecrease;
            }

            State = true;
        }

        public void Disable()
        {
            if (_moreControlsButton is not null)
            {
                _moreControlsButton.OnButtonPressed -= HandleValueIncrease;
            }
            
            if (_lessControlsButton is not null)
            {
                _lessControlsButton.OnButtonPressed -= HandleValueDecrease;
            }

            State = false;
        }

        private void HandleValueIncrease()
        {
            var newValue = _currentValue + _deltaPerClick;

            newValue = newValue > _maxValue ? _maxValue : newValue;

            _currentValue = newValue;
            
            OnValueChanged?.Invoke(newValue);
        }

        private void HandleValueDecrease()
        {
            var newValue = _currentValue - _deltaPerClick;

            newValue = newValue < _minValue ? _minValue : newValue;

            _currentValue = newValue;
            
            OnValueChanged?.Invoke(newValue);
        }
    }
}