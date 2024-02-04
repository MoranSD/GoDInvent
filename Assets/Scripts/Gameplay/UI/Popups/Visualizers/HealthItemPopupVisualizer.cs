namespace Gameplay.UI.Popups
{
    public class HealthItemPopupVisualizer : ItemPopupVisualizer
    {
        public override void DisplayMenu(ItemPopupMenu popupMenu)
        {
            actionButtonTitle = "Лечить";

            base.DisplayMenu(popupMenu);
        }
    }
}
