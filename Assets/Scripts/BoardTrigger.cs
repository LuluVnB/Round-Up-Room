using UnityEngine;

public class BoardTrigger : MonoBehaviour {
    [SerializeField] private CommunicationBoardUI boardUI;

    private void Reset() {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Nurse"))
            boardUI.Show(true);   // open when the nurse touches
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Nurse"))
            boardUI.Show(false);  // close when the nurse steps away
    }
}
