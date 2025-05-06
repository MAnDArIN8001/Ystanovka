using System;
using UnityEngine;

namespace Ystanovka.Controls.ControlsButton
{
    public class ControlButton : MonoBehaviour
    {
        public event Action OnButtonPressed;

        [SerializeField] private AudioSource _audio;

        private void OnMouseDown()
        {
            Press();
        }

        public void Press()
        {
            _audio.Play();
            
            OnButtonPressed?.Invoke();
        }
    }
}