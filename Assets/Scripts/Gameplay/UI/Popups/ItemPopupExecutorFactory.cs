using Gameplay.InventorySystem.Items;
using Gameplay.InventorySystem;
using Zenject;

namespace Gameplay.UI.Popups
{
    public class ItemPopupExecutorFactory
    {
        private DiContainer _diContainer;
        public ItemPopupExecutorFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        public IItemPopupExecutor Create(Stack itemStack, ItemPopupMenuService popupMenuService)
        {
            if (itemStack.item is IArmor)
            {
                var playerHealth = _diContainer.Resolve<FightSystem.Health.PlayerHealth>();
                return InitExecutor(new ArmorItemPopupExecutor(playerHealth), itemStack, popupMenuService);
            }
            if (itemStack.item is IBullet) return InitExecutor(new BulletItemPopupExecutor(), itemStack, popupMenuService);
            if (itemStack.item is IHealth)
            {
                var playerHealth = _diContainer.Resolve<FightSystem.Health.PlayerHealth>();
                return InitExecutor(new HealthItemPopupExecutor(playerHealth), itemStack, popupMenuService);
            }

            return null;
        }
        private IItemPopupExecutor InitExecutor(IItemPopupExecutor executor, Stack itemStack, ItemPopupMenuService popupMenuService)
        {
            executor.Init(itemStack, popupMenuService);
            return executor;
        }
    }
}
