using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Gameplay.InventorySystem.UI
{
    public class DisplayCell : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _count;

        public void SetVisual(Items.IItem item)
        {
            _icon.sprite = item.icon;
            _icon.color = Color.white;

            _count.text = item.count.ToString();
            _count.gameObject.SetActive(item.isStackable);
        }
        public void Clear()
        {
            _icon.sprite = null;
            _icon.color = new Color(0, 0, 0, 0);
            _count.gameObject.SetActive(false);
        }
    }
}
