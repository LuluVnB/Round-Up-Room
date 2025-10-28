using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler {
    public string slotCategory;
    public MiniGameManager gameManager;

    public void OnDrop(PointerEventData eventData) {
        DraggableEmoji emoji = eventData.pointerDrag.GetComponent<DraggableEmoji>();

        if (emoji != null) {
            //here to check if emoji category matches slot category
            if (emoji.emojiCategory == slotCategory) {
                //correct placement
                emoji.transform.SetParent(transform);
                emoji.GetComponent<CanvasGroup>().blocksRaycasts = false; //prevents re-dragging

                if (gameManager != null) {
                    gameManager.OnCorrectPlacement();
                }
            } else {
                //if wrong placement - return to original position
                emoji.ResetPosition();
            }
        }
    }
}