using System;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Table.Row
{
    public class Row : MonoBehaviour
    {
        private string _value;
        
        [SerializeField] private TMP_Text _startTemperature;
        [SerializeField] private TMP_Text _endTemperature;
        [SerializeField] private TMP_Text _power;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _outsideTemperature;
        [SerializeField] private TMP_Text _waternesy;
        [SerializeField] private TMP_Text _deltaTemperature;

        [Space, SerializeField] private GameObject _inputForm;

        [Space, SerializeField] private Button _okButton;
        [SerializeField] private Button _autoButton;

        [Space, SerializeField] private TMP_InputField _input; 

        public void Initialize(string startT, string endT, string power, string time, string outsideT, string waternesy, string deltaT)
        {
            _startTemperature.text = startT + "C";
            _endTemperature.text = endT + "C";
            _power.text = power + "Вт";
            _time.text = time + "мин";
            _outsideTemperature.text = outsideT + "C";
            _waternesy.text = waternesy + "%";
            _deltaTemperature.text = deltaT + "C";
            
            _inputForm.SetActive(true);
            _deltaTemperature.gameObject.SetActive(false);
            
            _input.onValueChanged.AddListener(HandleTextChanged);
            
            _okButton.onClick.AddListener(HandleOk);
            _autoButton.onClick.AddListener(HandleAuto);
        }

        private void HandleTextChanged(string newText)
        {
            string pattern = @"^[+-]?(\d+(\.\d*)?|\.\d+)$";

            if (!string.IsNullOrEmpty(newText) && Regex.IsMatch(newText, pattern))
            {
                _value = newText;
                
                return;   
            }

            StringBuilder result = new StringBuilder();
            bool hasDot = false;
            bool hasSign = false;

            foreach (char c in newText)
            {
                if (char.IsDigit(c))
                {
                    result.Append(c);
                }
                else if (c == '.' && !hasDot)
                {
                    result.Append(c);
                    hasDot = true;
                }
                else if ((c == '+' || c == '-') && !hasSign && result.Length == 0)
                {
                    result.Append(c);
                    hasSign = true;
                }
            }

            _input.text = newText;
        }

        private void HandleOk()
        {
            if (string.IsNullOrEmpty(_value))
            {
                return;
            }
            
            _deltaTemperature.text = _value + "C";
            
            _inputForm.SetActive(false);
            _deltaTemperature.gameObject.SetActive(true);
            
            _input.onValueChanged.RemoveListener(HandleTextChanged);
            
            _okButton.onClick.RemoveListener(HandleOk);
            _okButton.onClick.RemoveListener(HandleAuto);
        }

        private void HandleAuto()
        {
            _inputForm.SetActive(false);
            _deltaTemperature.gameObject.SetActive(true);
            
            _input.onValueChanged.RemoveListener(HandleTextChanged);
            
            _okButton.onClick.RemoveListener(HandleOk);
            _okButton.onClick.RemoveListener(HandleAuto);
        }
    }
}
