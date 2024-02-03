using Gameplay.FightSystem;

namespace Gameplay.InventorySystem.Items
{
    public interface IBullet : IItem
    {
        public BulletType type { get; }
    }
}
