using Gameplay.InventorySystem.Items;
using UnityEngine;

namespace Gameplay.InventorySystem.Data
{
    [CreateAssetMenu(menuName = "Game/Data/Items/Health")]
    public class ItemHealthConfig : ItemConfig
    {
        [field: SerializeField] public int healthPoints { get; private set; }
        public override IItem CreateItem()
        {
            var item = new HealthItem();
            item.healthPoints = healthPoints;

            InitBaseStats(item);
            return item;
        }
    }
}
