using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableEmoji : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public string emojiCategory; // "happy", "sad", "angry", "tired", "sick"

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Start() {
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //to check if dropped on a valid slot
        bool droppedOnSlot = false;

        if (eventData.pointerEnter != null) {
            DropSlot slot = eventData.pointerEnter.GetComponent<DropSlot>();
            if (slot != null && slot.slotCategory == emojiCategory) {
                droppedOnSlot = true;
                //don't return to original position, stay in slot
            }
        }

        //if not dropped on valid slot, return to original position
        if (!droppedOnSlot) {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void ResetPosition() {
        if (originalParent != null) {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}