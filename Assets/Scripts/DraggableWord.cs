using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] private Text label;

    private Transform originalParent;
    private Canvas rootCanvas;
    private CanvasGroup cg;

    private void Awake() {
        rootCanvas = GetComponentInParent<Canvas>();
        cg = gameObject.GetComponent<CanvasGroup>();
        if (!cg) cg = gameObject.AddComponent<CanvasGroup>();
    }

    public void SetText(string t) {
        if (label == null) label = GetComponentInChildren<Text>();
        label.text = t;
        name = "Word_" + t;
    }

    public void OnBeginDrag(PointerEventData e) {
        originalParent = transform.parent;
        cg.blocksRaycasts = false;           // allow DropZones to receive the drop
        transform.SetParent(rootCanvas.transform, true);
    }

    public void OnDrag(PointerEventData e) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rootCanvas.transform as RectTransform,
            e.position, e.pressEventCamera, out var localPos);
        (transform as RectTransform).anchoredPosition = localPos;
    }

    public void OnEndDrag(PointerEventData e) {
        cg.blocksRaycasts = true;
        // If not dropped on a DropZone, snap back to where it came from
        if (transform.parent == rootCanvas.transform)
            transform.SetParent(originalParent, false);
    }
}
