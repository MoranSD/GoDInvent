using Gameplay.InventorySystem.Data;
using Gameplay.InventorySystem.UI;
using Gameplay.InventorySystem;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class MetaInstaller : MonoInstaller
    {
        [SerializeField] private InventoryConfig _inventoryConfig;
        [SerializeField] private InventoryDisplay _inventoryDisplay;
        [SerializeField] private ItemsDataHandler _itemsDataHandler;
        public override void InstallBindings()
        {
            InstallInventory();
        }
        private void InstallInventory()
        {
            Container.Bind<Inventory>().AsSingle().WithArguments(_inventoryConfig, _inventoryDisplay, _itemsDataHandler);
        }
    }
}
