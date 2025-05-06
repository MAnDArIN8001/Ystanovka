using UnityEngine;

namespace UI
{
    public class PanelButton : MonoBehaviour
    {
        [SerializeField] private Panel _panel;

        public void ShowPanel()
        {
            gameObject.SetActive(false);
            _panel.Show();
        }

        public void ShowBtn()
        {
            gameObject.SetActive(true);
        }
    }
}