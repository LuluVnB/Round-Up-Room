using UnityEngine;

public class Inventory : MonoBehaviour {
    public bool[] isFull;
    public GameObject[] slots;

    private void Awake() {
        EnsureSize();
    }

#if UNITY_EDITOR
    private void OnValidate() {
        EnsureSize();
    }
#endif

    private void EnsureSize() {
        if (slots == null) return;
        if (isFull == null || isFull.Length != slots.Length)
            isFull = new bool[slots.Length];
    }
}
