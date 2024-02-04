using UnityEngine;

namespace Gameplay.InventorySystem.Data
{
    //[CreateAssetMenu(menuName = "Game/Data/Items/")]
    public abstract class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public string itemName { get; protected set; }
        [field: SerializeField] public Items.ItemType itemType { get; protected set; }
        [field: SerializeField] public int id { get; protected set; }
        [field: SerializeField] public Sprite icon { get; protected set; }
        [field: SerializeField] public bool isStackable { get; protected set; }
        [field: SerializeField] public int maxCount { get; protected set; }
        [field: SerializeField] public int startCount { get; protected set; }

        public abstract Items.IItem CreateItem();
        protected void InitBaseStats(Items.Item item)
        {
            item.name = itemName;
            item.itemType = itemType;
            item.id = id;
            item.icon = icon;
            item.isStackable = isStackable;
            item.maxCount = maxCount;
            item.count = startCount;
        }
    }
}
