using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ystanovka.Systems.StateView
{
    public class StateSystemView : MonoBehaviour
    {
        [SerializeField] private string _textTemplate;
        
        [Space, SerializeField] private AbstractSystem<int> _system;

        [Space, SerializeField] private List<string> _states;

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

        private void HandleValueUpdate(int newValue)
        {
            _text.text = $"{_textTemplate} {_states[newValue]}";
            _uiText.text = _states[newValue];
        }
    }
}