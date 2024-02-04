using Gameplay.InventorySystem.Items;

namespace Gameplay.UI.Popups
{
    public class HealthItemPopupExecutor : ItemPopupExecutor
    {
        private FightSystem.Health.PlayerHealth _playerHealth;
        public HealthItemPopupExecutor(FightSystem.Health.PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }
        public override void OnPressActionButton()
        {
            if (itemStack.item is IHealth healthItem)
            {
                if (itemStack.TryGet(1))
                {
                    _playerHealth.Heal(healthItem.healthPoints);
                }
            }
        }
    }
}
