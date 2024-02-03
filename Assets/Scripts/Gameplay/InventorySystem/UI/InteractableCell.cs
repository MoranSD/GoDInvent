using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;

namespace Gameplay.InventorySystem.UI
{
    public class InteractableCell : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public Vector2Int position { get; private set; }
        public bool isDrag { get; private set; }
        public Inventory inventory { get; private set; }

        private RectTransform _rectTF;
        private CanvasGroup _canvasGroup;

        [Inject]
        private void Construct(Inventory inventory)
        {
            this.inventory = inventory;
        }

        private void Awake()
        {
            _rectTF = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public void SetStats(Vector2Int position)
        {
            this.position = position;
        }
        public void OnPointerClick(PointerEventData eventData)
        {

        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (inventory.HasItem(position) == false) return;
            isDrag = true;
            _canvasGroup.blocksRaycasts = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (isDrag == false) return;

            transform.position = Input.mousePosition;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDrag == false) return;

            isDrag = false;
            _rectTF.anchoredPosition = Vector2.zero;
            _canvasGroup.blocksRaycasts = true;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;

            var cell = eventData.pointerDrag.GetComponent<InteractableCell>();
            inventory.Move(cell.position, position);
        }
    }
}
