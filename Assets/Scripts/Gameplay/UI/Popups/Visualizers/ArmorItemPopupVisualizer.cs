namespace Gameplay.UI.Popups
{
    public class ArmorItemPopupVisualizer : ItemPopupVisualizer
    {
        public override void DisplayMenu(ItemPopupMenu popupMenu)
        {
            actionButtonTitle = "Экипировать";

            base.DisplayMenu(popupMenu);
        }
    }
}
