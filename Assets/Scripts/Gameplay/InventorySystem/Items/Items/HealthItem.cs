namespace Gameplay.InventorySystem.Items
{
    [System.Serializable]
    public class HealthItem : Item, IHealth
    {
        public int healthPoints { get; set; }
    }
}
