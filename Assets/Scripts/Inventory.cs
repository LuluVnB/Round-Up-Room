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
    public int CountItems() {
        if (isFull == null) return 0;
        int c = 0;
        for (int i = 0; i < isFull.Length; i++)
            if (isFull[i]) c++;
        return c;
    }

    //returns how many items were removed.
    public int ClearAll() {
        if (slots == null || isFull == null) return 0;

        int removed = 0;
        for (int i = 0; i < slots.Length; i++) {
            if (!isFull[i]) continue;

            //removes any UI item(s) parented to the slot
            var t = slots[i].transform;
            for (int k = t.childCount - 1; k >= 0; k--)
                GameObject.Destroy(t.GetChild(k).gameObject);

            isFull[i] = false;
            removed++;
        }
        return removed;
    }

}
