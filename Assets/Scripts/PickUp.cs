using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour {
    public GameObject itemButton; // assign in Inspector

    private void Reset() {
        // Make sure this collider is a trigger for trigger events
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
                Instantiate(itemButton, inventory.slots[i].transform, false);
                Destroy(gameObject);
                return;
            }
        }

        Debug.Log("Inventory full.");
    }
}
