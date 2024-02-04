using Gameplay.InventorySystem.Items;

namespace Gameplay.UI.Popups
{
    public abstract class ItemPopupVisualizer : IItemPopupVisualizer
    {
        protected IItem item;
        protected string actionButtonTitle;

        public void Init(IItem item) => this.item = item;
        public virtual void DisplayMenu(ItemPopupMenu popupMenu)
        {
            popupMenu.itemName.text = item.name;
            popupMenu.itemIcon.sprite = item.icon;
            popupMenu.actionButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = actionButtonTitle;
            popupMenu.deleteButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Удалить";
        }
    }
}
