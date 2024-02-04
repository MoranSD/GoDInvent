using Gameplay.InventorySystem.UI;
using Gameplay.InventorySystem;
using UnityEngine.UI;
using UnityEngine;
using Zenject;
using TMPro;

namespace Gameplay.UI.Popups
{
    public class ItemPopupMenuService : MonoBehaviour
    {
        public bool isMenuOpened { get; private set; }

        [SerializeField] private GameObject _itemPopupObject;
        [SerializeField] private ItemPopupMenu _itemPopupMenu;

        private Inventory _inventory;
        private ItemPopupExecutorFactory _executorFactory;

        private IItemPopupVisualizer _currentVisualizer;
        private IItemPopupExecutor _currentExecutor;

        [Inject]
        private void Construct(Inventory inventory, ItemPopupExecutorFactory executorFactory)
        {
            _inventory = inventory;
            _executorFactory = executorFactory;
        }

        private void Start()
        {
            _itemPopupObject.SetActive(false);
            InteractableCell.onClickEvent += OnClickOnItemCell;
        }
        private void OnDestroy()
        {
            InteractableCell.onClickEvent -= OnClickOnItemCell;
        }
        public void ClosePopup()
        {
            if (isMenuOpened == false) return;

            _itemPopupObject.SetActive(false);
            isMenuOpened = false;
            _currentVisualizer = null;
            _currentExecutor = null;
        }
        public void OnPressActionButton()
        {
            if (_currentExecutor == null) return;
            _currentExecutor.OnPressActionButton();
            ClosePopup();
        }
        public void OnPressDeleteButton()
        {
            if (_currentExecutor == null) return;
            _currentExecutor.OnPressDeleteButton();
            ClosePopup();
        }
        private void OnClickOnItemCell(Vector2Int cellPosition)
        {
            if (isMenuOpened || _inventory.HasItem(cellPosition) == false) return;
            isMenuOpened = true;
            _itemPopupObject.SetActive(true);

            var stack = _inventory.GetStack(cellPosition);

            _currentVisualizer = ItemPopupVisualizerFactory.Create(stack.item);
            _currentVisualizer.DisplayMenu(_itemPopupMenu);

            _currentExecutor = _executorFactory.Create(stack, this);
        }
    }

    [System.Serializable]
    public struct ItemPopupMenu
    {
        public TextMeshProUGUI itemName;
        public Image itemIcon;
        public Button actionButton;
        public Button deleteButton;
    }
}
