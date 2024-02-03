using UnityEngine;

namespace Gameplay.InventorySystem.Items
{
    public abstract class Item : IItem
    {
        public string name { get; set; }
        public Sprite icon { get; set; }
        public int id { get; set; }
        public bool isStackable { get; set; }
        public int maxCount { get; set; }
        public int count { get; set; }
    }
}
