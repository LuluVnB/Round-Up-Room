using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData e) {
        var drag = e.pointerDrag ? e.pointerDrag.GetComponent<DraggableWord>() : null;
        if (drag == null) return;

        drag.transform.SetParent(transform, false);
        var rt = drag.transform as RectTransform;
        rt.anchoredPosition = Vector2.zero;  // center in the slot/list
    }
}
