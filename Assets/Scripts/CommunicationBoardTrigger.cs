using UnityEngine;

public class CommunicationBoardTrigger : MonoBehaviour {
    public GameObject miniGameUI;
    public NurseScript nurseScript;

    private void Start() {
        // Make sure mini game is hidden at start
        if (miniGameUI != null) {
            miniGameUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if the nurse entered the trigger
        if (other.CompareTag("Player")) {
            OpenMiniGame();
        }
    }

    void OpenMiniGame() {
        // Show the mini game UI
        if (miniGameUI != null) {
            miniGameUI.SetActive(true);

            // Disable nurse movement
            if (nurseScript != null) {
                nurseScript.enabled = false;
            }

            // Pause the game (optional - set to 0f if you want full pause)
            // Note: UI interactions need timeScale > 0, so we don't pause
            // Time.timeScale = 0f;
        }
    }

    public void CloseMiniGame() {
        // Hide the mini game UI
        if (miniGameUI != null) {
            miniGameUI.SetActive(false);

            // Re-enable nurse movement
            if (nurseScript != null) {
                nurseScript.enabled = true;
            }

            // Resume the game if it was paused
            // Time.timeScale = 1f;
        }
    }
}