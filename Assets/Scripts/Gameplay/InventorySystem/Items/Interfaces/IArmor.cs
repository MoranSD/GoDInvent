using FightSystem.Health;

namespace Gameplay.InventorySystem.Items
{
    public interface IArmor : IItem
    {
        public int armorPoints { get; }
        public ArmorType armorType { get; }
    }
}
