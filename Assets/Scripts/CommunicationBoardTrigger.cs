using UnityEngine;

public class CommunicationBoardTrigger : MonoBehaviour {
    public GameObject miniGameUI;
    public NurseScript nurseScript;

    private void Start() {
        //hide to make sure mini game is hidden at start
        if (miniGameUI != null) {
            miniGameUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //check if the nurse entered the trigger using the player tag
        if (other.CompareTag("Player")) {
            OpenMiniGame();
        }
    }

    void OpenMiniGame() {
        //reveal the mini game UI
        if (miniGameUI != null) {
            miniGameUI.SetActive(true);

            //disable nurse movement
            if (nurseScript != null) {
                nurseScript.enabled = false;
            }
        }
    }

    public void CloseMiniGame() {
        //hide the mini game UI
        if (miniGameUI != null) {
            miniGameUI.SetActive(false);

            if (nurseScript != null) {
                nurseScript.enabled = true;
            }

        }
    }
}