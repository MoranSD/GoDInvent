using Gameplay.InventorySystem.Items;

namespace Gameplay.UI.Popups
{
    public interface IItemPopupVisualizer
    {
        public void Init(IItem item);
        public void DisplayMenu(ItemPopupMenu popupMenu);
    }
}
