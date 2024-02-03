using Gameplay.InventorySystem.Items;
using Gameplay.InventorySystem.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.InventorySystem
{
    public class Inventory: Zenject.IInitializable
    {
        public InventoryConfig config { get; private set; }

        private Dictionary<Vector2Int, Stack> _stacks;

        private UI.InventoryDisplay _display;

        public Inventory(InventoryConfig config, UI.InventoryDisplay display)
        {
            _display = display;
            this.config = config;

            _stacks = new Dictionary<Vector2Int, Stack>();
        }

        public void Initialize()
        {
            for (int i = 0; i < config.startItems.Length; i++)
            {
                TryAddItem(config.startItems[i].itemConfig.CreateItem(), config.startItems[i].position);
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
    }
}
