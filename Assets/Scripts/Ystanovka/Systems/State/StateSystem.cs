using System;
using UnityEngine;
using Ystanovka.Controls;
using Ystanovka.Controls.ControlsButton;

namespace Ystanovka.Systems.StateView
{
    public class StateSystem : AbstractSystem<int>, IStatable
    {
        public override event Action<int> OnValueChanged;
        
        private int _currentIndex;
        [SerializeField] private int _maxValue;

        [Space, SerializeField] private ControlButton _moreButton;
        [SerializeField] private ControlButton _lessButton;

        public int CurrentIndex => _currentIndex;

        public bool State { get; private set; }
        
        public void Enable()
        {
            if (_moreButton is not null)
            {
                _moreButton.OnButtonPressed += HandleValueIncrease;
            }
            
            if (_lessButton is not null)
            {
                _lessButton.OnButtonPressed += HandleValueDecrease;
            }

            State = true;
        }

        public void Disable()
        {
            if (_moreButton is not null)
            {
                _moreButton.OnButtonPressed -= HandleValueIncrease;
            }
            
            if (_lessButton is not null)
            {
                _lessButton.OnButtonPressed -= HandleValueDecrease;
            }

            State = false;
        }
        
        private void HandleValueIncrease()
        {
            var newValue = _currentIndex + 1;

            newValue = newValue > _maxValue ? _maxValue : newValue;

            _currentIndex = newValue;
            
            OnValueChanged?.Invoke(newValue);
        }

        private void HandleValueDecrease()
        {
            var newValue = _currentIndex - 1;

            newValue = newValue < 0 ? 0 : newValue;

            _currentIndex = newValue;
            
            OnValueChanged?.Invoke(newValue);
        }
    }
}