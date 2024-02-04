namespace Gameplay.UI.Popups
{
    public class BulletItemPopupVisualizer : ItemPopupVisualizer
    {
        public override void DisplayMenu(ItemPopupMenu popupMenu)
        {
            actionButtonTitle = "Купить";

            base.DisplayMenu(popupMenu);
        }
    }
}
