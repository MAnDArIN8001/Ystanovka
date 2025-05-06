using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ystanovka;
using Ystanovka.Controls.ControlsButton;

namespace UI
{
    public class PowerButton : MonoBehaviour
    {
        [SerializeField] private ControlButton _controlButton;

        [Space, SerializeField] private TMP_Text _text;

        [Space, SerializeField] private Conder _conder;

        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_button is not null)
            {
                _button.onClick.AddListener(Press);
            }

            if (_controlButton is not null)
            {
                _controlButton.OnButtonPressed += HandlePress;
            }
        }

        private void OnDisable()
        {
            if (_button is not null)
            {
                _button.onClick.RemoveListener(Press);
            }
            
            if (_controlButton is not null)
            {
                _controlButton.OnButtonPressed -= HandlePress;
            }
        }

        private void Press()
        {
            _controlButton.Press();
        }

        private void HandlePress()
        {
            _text.text = _conder.Power ? "Выкл" : "Вкл";
        }
    }
}