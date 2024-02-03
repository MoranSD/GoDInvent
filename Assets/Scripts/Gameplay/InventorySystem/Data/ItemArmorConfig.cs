using Gameplay.FightSystem.Health;
using Gameplay.InventorySystem.Items;
using UnityEngine;

namespace Gameplay.InventorySystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/Items/Armor")]
    public class ItemArmorConfig : ItemConfig
    {
        [field: SerializeField] public int armorPoints { get; private set; }
        [field: SerializeField] public ArmorType armorType { get; private set; }

        public override IItem CreateItem()
        {
            var item = new ArmorItem();
            item.armorPoints = armorPoints;
            item.armorType = armorType;

            InitBaseStats(item);
            return item;
        }
    }
}
