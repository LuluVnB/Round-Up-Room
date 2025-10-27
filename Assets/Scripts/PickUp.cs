using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour {
    public GameObject itemButton; //assign in Inspector, must use a UI object with RectTransform or it won't work

    private void Reset() {

        var c = GetComponent<Collider2D>();
        if (c != null) c.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory == null) {
            Debug.LogWarning("Player has no Inventory component.");
            return;
        }

        if (itemButton == null) {
            Debug.LogWarning("PickUp: Item Button prefab not assigned.");
            return;
        }

        for (int i = 0; i < inventory.slots.Length; i++) {
            if (!inventory.isFull[i]) {
                inventory.isFull[i] = true;

                //parent UI item to the slot
                Transform parent = inventory.slots[i].transform;
                GameObject go = Instantiate(itemButton, parent, false);

                //it's proper UI and fits the slot
                var rt = go.GetComponent<RectTransform>();
                if (rt != null) {
                    rt.anchorMin = Vector2.zero;
                    rt.anchorMax = Vector2.one;
                    rt.offsetMin = Vector2.zero;   //left/bottom
                    rt.offsetMax = Vector2.zero;   //right/top
                    rt.anchoredPosition = Vector2.zero;
                    rt.localScale = Vector3.one;
                    go.transform.SetAsLastSibling(); //on top
                } else {
                    Debug.LogWarning("ItemButton prefab must be a UI object with RectTransform.");
                }

                Destroy(gameObject);
                return;
            }
        }


        Debug.Log("Inventory full.");
    }
}
