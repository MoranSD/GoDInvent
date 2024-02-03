using UnityEngine;

namespace Gameplay.InventorySystem.Items
{
    public interface IItem
    {
        public string name { get; }
        public Sprite icon { get; }
        public int id { get; }
        public bool isStackable { get; }
        public int maxCount { get; }
        public int count { get; set; }
    }
}
