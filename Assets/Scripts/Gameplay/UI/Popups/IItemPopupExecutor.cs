using Gameplay.InventorySystem;

namespace Gameplay.UI.Popups
{
    public interface IItemPopupExecutor
    {
        public void Init(Stack itemStack, ItemPopupMenuService popupMenuService);
        public void OnPressActionButton();
        public void OnPressDeleteButton();
    }
}
