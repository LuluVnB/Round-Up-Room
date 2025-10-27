using UnityEngine;

public class Inventory : MonoBehaviour {
    [Tooltip("Mark true when a slot is filled.")]
    public bool[] isFull;

    [Tooltip("UI slot objects (same length as isFull).")]
    public GameObject[] slots;

    private void OnValidate() {
        if (slots != null && isFull != null && slots.Length != isFull.Length) {
            Debug.LogWarning("Inventory: 'slots' and 'isFull' must be the same length.");
        }
    }
}
