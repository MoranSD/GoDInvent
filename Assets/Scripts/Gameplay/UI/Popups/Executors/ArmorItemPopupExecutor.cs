using Gameplay.InventorySystem.Items;

namespace Gameplay.UI.Popups
{
    public class ArmorItemPopupExecutor : ItemPopupExecutor
    {
        private FightSystem.Health.PlayerHealth _playerHealth;
        public ArmorItemPopupExecutor(FightSystem.Health.PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }
        public override void OnPressActionButton()
        {
            if (itemStack.item is IArmor armorItem)
            {
                if (_playerHealth.HasArmor(armorItem.armorType))
                {
                    var playerArmor = _playerHealth.GetArmor(armorItem.armorType);
                    var inventory = itemStack.inventory;
                    var stackPosition = itemStack.position;
                    if (itemStack.TryGet(1))
                    {
                        _playerHealth.SetArmor(armorItem);
                        inventory.TryAddItem(playerArmor, stackPosition);
                    }
                }
                else
                {
                    if (itemStack.TryGet(1))
                    {
                        _playerHealth.SetArmor(armorItem);
                    }
                }
            }
        }
    }
}
