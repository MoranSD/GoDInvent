using System.Collections.Generic;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Gameplay.InventorySystem.UI
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private Dictionary<Vector2Int, DisplayCell> _cells;

        private UIInventoryCellFactory _cellFactory;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory, UIInventoryCellFactory cellFactory)
        {
            _cells = new();

            _cellFactory = cellFactory;
            _inventory = inventory;
        }

        private void Start()
        {
            CreateCells();
        }

        public void UpdateCell(Vector2Int cellPosition)
        {
            if (_cells.ContainsKey(cellPosition) == false) return;

            var cell = _cells[cellPosition];

            if (_inventory.HasItem(cellPosition))
            {
                var item = _inventory.GetItem(cellPosition);
                cell.SetVisual(item);
            }
            else
            {
                cell.Clear();
            }
        }
        private void CreateCells()
        {
            var size = _inventory.config.size;

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var cellPosition = new Vector2Int(x, y);
                    var item = _inventory.HasItem(cellPosition) ? _inventory.GetItem(cellPosition) : null;
                    var cell = _cellFactory.Create(_container, cellPosition, item);

                    _cells.Add(cellPosition, cell);
                }
            }
        }
    }
}
