using System;
using TMPro;
using UnityEngine;

namespace Ystanovka.Systems
{
    public class SystemView : MonoBehaviour
    {
        [SerializeField] private string _textTemplate;
        [SerializeField] private string _aditionalValue;
        
        [Space, SerializeField] private AbstractSystem<float> _system;

        [Space, SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _uiText;

        private void OnEnable()
        {
            if (_system is not null)
            {
                _system.OnValueChanged += HandleValueUpdate;
            }
        }

        private void OnDisable()
        {
            if (_system is not null)
            {
                _system.OnValueChanged -= HandleValueUpdate;
            }
        }

        private void HandleValueUpdate(float newValue)
        {
            _text.text = $"{_textTemplate}{newValue.ToString()}{_aditionalValue}";
            _uiText.text = newValue.ToString();
        }
    }
}