using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour {
    public GameObject itemButton;             //UI item prefab
    [Header("Feedback")]
    public ParticleSystem sparkleFX;          
    public ParticleSystem confettiFX;         
    public AudioClip pickupSFX;               
    [Range(0f, 1f)] public float sfxVolume = 0.9f;
    public Vector2 fxOffset = new Vector2(0f, 0.1f); //slight lift from floor

    void Reset() {
        var c = GetComponent<Collider2D>();
        if (c) c.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<Inventory>();
        if (inventory == null) return;

        for (int i = 0; i < inventory.slots.Length; i++) {
            if (!inventory.isFull[i]) {
                //added to inventory UI
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform);

                
                PlayPickupFeedback();

                
                var col = GetComponent<Collider2D>();
                if (col) col.enabled = false;
                var sr = GetComponent<SpriteRenderer>();
                if (sr) sr.enabled = false;

                Destroy(gameObject); //this will remove the floor item
                return;
            }
        }
    }

    void PlayPickupFeedback() {
        Vector3 pos = transform.position + (Vector3)fxOffset;

        
        if (pickupSFX) AudioSource.PlayClipAtPoint(pickupSFX, pos, sfxVolume);

        // Sparkle
        if (sparkleFX) {
            var s = Instantiate(sparkleFX, pos, Quaternion.identity);
            AutoDestroyParticles(s);
        }


    }

    void AutoDestroyParticles(ParticleSystem ps) {
        var main = ps.main;
        float extra = 0.5f; 
        float life =
            main.duration +
            (main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants
                ? main.startLifetime.constantMax
                : main.startLifetime.constant);
        Destroy(ps.gameObject, life + extra);
    }
}
