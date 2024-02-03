using Gameplay.FightSystem;

namespace Gameplay.InventorySystem.Items
{
    [System.Serializable]
    public class BulletItem : Item, IBullet
    {
        public BulletType type { get; set; }
    }
}
