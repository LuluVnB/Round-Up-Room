using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [Header("Assign the TMP child here")]
    [SerializeField] private TextMeshProUGUI label;

    private Transform originalParent;
    private Canvas rootCanvas;
    private CanvasGroup cg;

    private void Awake() {
        rootCanvas = GetComponentInParent<Canvas>();
        cg = GetComponent<CanvasGroup>();
        if (!cg) cg = gameObject.AddComponent<CanvasGroup>();

        // Auto-find if not assigned in Inspector
        if (!label) label = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void SetText(string t) {
        if (!label) label = GetComponentInChildren<TextMeshProUGUI>(true);
        if (label) label.text = t;
        name = "Word_" + t;
    }

    public void OnBeginDrag(PointerEventData e) {
        originalParent = transform.parent;
        cg.blocksRaycasts = false;
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
        if (transform.parent == rootCanvas.transform)
            transform.SetParent(originalParent, false);
    }
}
