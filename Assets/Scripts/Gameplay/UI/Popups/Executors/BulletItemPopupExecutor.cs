using Gameplay.InventorySystem.Items;

namespace Gameplay.UI.Popups
{
    public class BulletItemPopupExecutor : ItemPopupExecutor
    {
        public override void OnPressActionButton()
        {
            if (itemStack.item is IBullet == false) return;

            itemStack.AddToMax();
        }
    }
}
