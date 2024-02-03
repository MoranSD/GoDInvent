namespace Gameplay.InventorySystem.Items
{
    public interface IHealth : IItem
    {
        public int healthPoints { get; }
    }
}
