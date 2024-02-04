using Gameplay.InventorySystem.Items;
using System;
using System.Linq;
using UnityEngine;

namespace Gameplay.InventorySystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/ItemsHandler")]
    public class ItemsDataHandler : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemsConfigs;

        public IItem CreateItem(Func<ItemConfig, bool> func)
        {
            return _itemsConfigs.First(func).CreateItem();
        }
    }
}
