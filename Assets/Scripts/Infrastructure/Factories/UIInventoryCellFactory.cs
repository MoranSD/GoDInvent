using Gameplay.InventorySystem.Items;
using Gameplay.InventorySystem.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class UIInventoryCellFactory
    {
        private DiContainer _diContainer;
        private GameObject _cellPrefab;
        public UIInventoryCellFactory(DiContainer diContainer, GameObject cellPrefab)
        {
            _diContainer = diContainer;
            _cellPrefab = cellPrefab;
        }
        public DisplayCell Create(Transform container, Vector2Int position, IItem item = null)
        {
            var cell = _diContainer.InstantiatePrefab(_cellPrefab, container).GetComponent<DisplayCell>();
            cell.GetComponentInChildren<InteractableCell>().SetStats(position);

            if (item == null) cell.Clear();
            else cell.SetVisual(item);

            return cell;
        }
    }
}
