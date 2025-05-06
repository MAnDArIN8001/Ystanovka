using UnityEngine;

namespace UI
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private PanelButton _panelButton;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _panelButton.ShowBtn();
        }
    }
}