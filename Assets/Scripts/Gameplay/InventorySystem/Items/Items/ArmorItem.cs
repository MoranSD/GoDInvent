using Gameplay.FightSystem.Health;

namespace Gameplay.InventorySystem.Items
{
    [System.Serializable]
    public class ArmorItem : Item, IArmor
    {
        public int armorPoints { get; set; }
        public ArmorType armorType { get; set; }
    }
}
