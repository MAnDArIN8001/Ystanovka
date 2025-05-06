using UnityEngine;

namespace UI.Table
{
    public class TableOpener : MonoBehaviour
    {
        [SerializeField] private TableController _table;

        public void Table()
        {
            var isOpened = _table.IsOpened;

            if (!isOpened)
            {
                _table.Show();
            }
            else
            {
                _table.Hide();
            }
        }
    }
}