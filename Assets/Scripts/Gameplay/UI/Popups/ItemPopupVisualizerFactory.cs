using Gameplay.InventorySystem.Items;

namespace Gameplay.UI.Popups
{
    public static class ItemPopupVisualizerFactory
    {
        public static IItemPopupVisualizer Create(IItem item)
        {
            if (item is IArmor) return InitVisualizer(new ArmorItemPopupVisualizer(), item);
            if (item is IBullet) return InitVisualizer(new BulletItemPopupVisualizer(), item);
            if (item is IHealth) return InitVisualizer(new HealthItemPopupVisualizer(), item);

            return null;
        }
        private static IItemPopupVisualizer InitVisualizer(IItemPopupVisualizer popupVisualizer, IItem item)
        {
            popupVisualizer.Init(item);
            return popupVisualizer;
        }
    }
}
