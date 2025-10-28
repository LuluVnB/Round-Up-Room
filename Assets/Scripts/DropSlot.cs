using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler {
    public string slotCategory; // "happy", "sad", "angry", "tired", "sick"
    public MiniGameManager gameManager;

    public void OnDrop(PointerEventData eventData) {
        DraggableEmoji emoji = eventData.pointerDrag.GetComponent<DraggableEmoji>();

        if (emoji != null) {
            // Check if emoji category matches slot category
            if (emoji.emojiCategory == slotCategory) {
                // Correct placement
                emoji.transform.SetParent(transform);
                emoji.GetComponent<CanvasGroup>().blocksRaycasts = false; // Prevent re-dragging

                if (gameManager != null) {
                    gameManager.OnCorrectPlacement();
                }
            } else {
                // Wrong placement - return to original position
                emoji.ResetPosition();
            }
        }
    }
}