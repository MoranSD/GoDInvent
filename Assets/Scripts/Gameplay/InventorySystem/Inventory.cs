using Gameplay.InventorySystem.Items;
using Gameplay.InventorySystem.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.InventorySystem
{
    public class Inventory
    {
        public InventoryConfig config { get; private set; }

        private Dictionary<Vector2Int, Stack> _stacks;

        private UI.InventoryDisplay _display;
        private ItemsDataHandler _itemsData;

        public Inventory(InventoryConfig config, UI.InventoryDisplay display, ItemsDataHandler itemsDataHandler)
        {
            _display = display;
            this.config = config;
            _itemsData = itemsDataHandler;

            _stacks = new Dictionary<Vector2Int, Stack>();
        }
        public void SetData(object data)
        {
            var inventoryData = (InventoryData[])data;

            for (int i = 0; i < inventoryData.Length; i++)
            {
                IItem item;
                InitItem(out item, inventoryData[i].itemType, inventoryData[i].itemAdditionalData);
                TryAddNewStack(item, new Vector2Int(inventoryData[i].xPosition, inventoryData[i].yPosition));
            }
        }
        public void ResetToDefault()
        {
            for (int i = 0; i < config.startItems.Length; i++)
            {
                TryAddItem(config.startItems[i].itemConfig.CreateItem(), config.startItems[i].position);
            }
        }
        public void Move(Vector2Int from, Vector2Int to)
        {
            if (from == to) return;

            if (HasItem(from) == false) return;

            bool isItemMoved = false;

            if (HasItem(to))
            {
                if (TryAddItem(GetItem(from), to)) 
                    isItemMoved = true;
            }
            else
            {
                if (TryAddNewStack(GetItem(from), to))
                    isItemMoved = true;
            }

            if (isItemMoved)
            {
                _stacks.Remove(from);
                _display.UpdateCell(from);
                _display.UpdateCell(to);
            }
        }
        public bool TryAddItem(IItem item, Vector2Int position)
        {
            if (HasItem(position))
            {
                if (item.isStackable)
                {
                    var stack = _stacks[position];
                    if (stack.TryAdd(item))
                    {
                        _display.UpdateCell(stack.position);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return TryAddNewStack(item, position);
            }
        }
        public bool TryAddItem(IItem item)
        {
            if (item.isStackable)
            {
                if (TryGetFreeStack(item.id, item.count, out var stack))
                {
                    if (stack.TryAdd(item))
                    {
                        _display.UpdateCell(stack.position);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return TryAddNewStack(item, new Vector2Int(-1, -1));
        }
        public Stack GetStack(Vector2Int position) => _stacks[position];
        public void Remove(Vector2Int position)
        {
            _stacks.Remove(position);
            _display.UpdateCell(position);
        }
        public bool HasItem(Vector2Int itemPosition) => _stacks.ContainsKey(itemPosition);
        public IItem GetItem(Vector2Int itemPosition) => _stacks[itemPosition].item;
        public bool TryGetItemStack(System.Func<Stack,bool> func, out Stack item)
        {
            item = _stacks.Values.FirstOrDefault(func);
            return item != null;
        }
        public void UpdateStack(Vector2Int position)
        {
            if (_stacks.ContainsKey(position) == false) return;

            if (_stacks[position].item.count == 0)
            {
                _stacks.Remove(position);
            }

            _display.UpdateCell(position);
        }
        public object GetData()
        {
            var inventoryData = new InventoryData[_stacks.Count];

            int itemCount = 0;
            foreach (var keiPair in _stacks)
            {
                inventoryData[itemCount].xPosition = keiPair.Key.x;
                inventoryData[itemCount].yPosition = keiPair.Key.y;
                inventoryData[itemCount].itemType = keiPair.Value.item.itemType;

                switch (keiPair.Value.item.itemType)
                {
                    case ItemType.bullet:
                        inventoryData[itemCount].itemAdditionalData = ((IBullet)keiPair.Value.item).type;
                        break;
                    case ItemType.armor:
                        inventoryData[itemCount].itemAdditionalData = ((IArmor)keiPair.Value.item).armorType;
                        break;
                    case ItemType.health:
                        inventoryData[itemCount].itemAdditionalData = ((IHealth)keiPair.Value.item).healthPoints;
                        break;
                    default:
                        break;
                }

                inventoryData[itemCount].count = keiPair.Value.item.count;

                itemCount++;
            }

            return inventoryData;
        }
        private void InitItem(out IItem item, ItemType itemType, object additionalData)
        {
            switch (itemType)
            {
                case ItemType.bullet:
                    item = _itemsData.CreateItem(
                        x =>
                        x is ItemBulletConfig bulletConfig &&
                        bulletConfig.type == (FightSystem.BulletType)additionalData);
                    break;
                case ItemType.armor:
                    item = _itemsData.CreateItem(
                        x =>
                        x is ItemArmorConfig armorConfig &&
                        armorConfig.armorType == (FightSystem.Health.ArmorType)additionalData);
                    break;
                case ItemType.health:
                    item = _itemsData.CreateItem(
                        x => x is ItemHealthConfig healthConfig);

                    ((HealthItem)item).healthPoints = (int)additionalData;
                    break;
                default:
                    item = null;
                    break;
            }
        }
        private bool TryAddNewStack(IItem item, Vector2Int position)
        {
            if (IsCanAddNewStack() == false || HasItem(position))
                return false;

            var stackPosition = (position.x == -1) ? GetFreePosition() : position;

            var itemStack = new Stack(stackPosition, item, this);
            _stacks.Add(stackPosition, itemStack);
            _display.UpdateCell(stackPosition);
            return true;
        }
        private bool IsCanAddNewStack()
        {
            int maxStacksCount = config.size.x * config.size.y;
            if (_stacks.Count >= maxStacksCount) return false;
            else return true;
        }
        private bool TryGetFreeStack(int itemId, int requiredSpace, out Stack stack)
        {
            stack = _stacks.Values.FirstOrDefault(x => x.item.id == itemId && x.remainingSpace >= requiredSpace);
            return stack != null;
        }
        private Vector2Int GetFreePosition()
        {
            for (int x = 0; x < config.size.x; x++)
            {
                for (int y = 0; y < config.size.y; y++)
                {
                    var freePosition = new Vector2Int(x, y);

                    if (_stacks.ContainsKey(freePosition)) continue;

                    return freePosition;
                }
            }

            return new Vector2Int(-1, -1);
        }

        [System.Serializable]
        private struct InventoryData
        {
            public int xPosition;
            public int yPosition;
            public ItemType itemType;
            public object itemAdditionalData;
            public int count;
        }
    }
}
