using Gameplay.InventorySystem;

namespace Gameplay.UI.Popups
{
    public abstract class ItemPopupExecutor : IItemPopupExecutor
    {
        protected Stack itemStack;
        protected ItemPopupMenuService popupMenuService;

        public void Init(Stack itemStack, ItemPopupMenuService popupMenuService)
        {
            this.itemStack = itemStack;
            this.popupMenuService = popupMenuService;
        }
        public abstract void OnPressActionButton();
        public void OnPressDeleteButton()
        {
            if (itemStack == null) return;
            itemStack.inventory.Remove(itemStack.position);
        }
    }
}
