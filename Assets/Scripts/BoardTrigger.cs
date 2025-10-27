using UnityEngine;

public class BoardTrigger : MonoBehaviour {
    [SerializeField] private CommunicationBoardUI boardUI;
    [SerializeField] private string targetTag = "Player"; 

    private void Reset() {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"BoardTrigger ENTER by {other.name} (tag={other.tag})", this);
        if (other.CompareTag(targetTag) && boardUI != null) boardUI.Show(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log($"BoardTrigger EXIT by {other.name} (tag={other.tag})", this);
        if (other.CompareTag(targetTag) && boardUI != null) boardUI.Show(false);
    }

}
