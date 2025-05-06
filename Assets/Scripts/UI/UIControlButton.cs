using UnityEngine;
using UnityEngine.UI;
using Ystanovka.Controls.ControlsButton;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UIControlButton : MonoBehaviour
    {
        [SerializeField] private ControlButton _controlButton;

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
        }

        private void OnDisable()
        {
            if (_button is not null)
            {
                _button.onClick.RemoveListener(Press);
            }
        }

        public void Press()
        {
            _controlButton.Press();
        }
    }
}