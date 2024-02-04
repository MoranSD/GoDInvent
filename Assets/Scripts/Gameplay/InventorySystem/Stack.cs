using UnityEngine;
using Gameplay.InventorySystem.Items;

namespace Gameplay.InventorySystem
{
    public class Stack
    {
        public int remainingSpace => item.maxCount - item.count;
        public Vector2Int position { get; private set; }
        public IItem item { get; private set; }
        public Inventory inventory { get; private set; }
        
        public Stack(Vector2Int position, IItem item, Inventory inventory)
        {
            this.position = position;
            this.item = item;
            this.inventory = inventory;
        }
        public void AddToMax()
        {
            if (item.count >= item.maxCount) return;

            item.count = item.maxCount;
            inventory.UpdateStack(position);
        }
        public bool TryAdd(IItem item)
        {
            if(item.isStackable == false)
            {
                Debug.Log("You trying to add to stackable item in stack");
                return false;
            }

            if(remainingSpace < this.item.count)
            {
                Debug.Log("Stack dont have enough space");
                return false;
            }

            this.item.count += item.count;
            return true;
        }
        public bool TryGet(int count)
        {
            if(item.count < count)
            {
                Debug.Log("Stack dont have enough count");
                return false;
            }
            
            item.count -= count;
            inventory.UpdateStack(position);
            return true;
        }
    }
}
