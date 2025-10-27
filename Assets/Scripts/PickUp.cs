using UnityEngine;

public class PickUp : MonoBehaviour {
    [Tooltip("UI prefab to drop into a slot (e.g., an ItemButton with Image).")]
    public GameObject itemButton;

    private void OnTriggerEnter2D(Collider2D other) {
        // Only react to the player
        if (!other.CompareTag("Player")) return;

        // Get the player's Inventory directly from the collider we hit
        var inventory = other.GetComponent<Inventory>();
        if (inventory == null) {
            Debug.LogWarning("Player has no Inventory component.");
            return;
        }

        // Find first empty slot
        for (int i = 0; i < inventory.slots.Length; i++) {
            if (!inventory.isFull[i]) {
                inventory.isFull[i] = true;

                // Parent UI item to the slot
                var parent = inventory.slots[i].transform;
                Instantiate(itemButton, parent, false);

                // Destroy the world pickup
                Destroy(gameObject);
                return;
            }
        }

        // Optional: inventory full feedback
        Debug.Log("Inventory full – cannot pick up item.");
    }
}
