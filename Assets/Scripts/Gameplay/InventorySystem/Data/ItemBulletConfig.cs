using Gameplay.InventorySystem.Items;
using Gameplay.FightSystem;
using UnityEngine;

namespace Gameplay.InventorySystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/Items/Bullet")]
    public class ItemBulletConfig : ItemConfig
    {
        [field: SerializeField] public BulletType type { get; private set; }

        public override IItem CreateItem()
        {
            var item = new BulletItem();
            item.type = type;

            InitBaseStats(item);
            return item;
        }
    }
}
