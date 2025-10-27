using UnityEngine;
using UnityEngine.UI;

public class HamperDepositImage : MonoBehaviour {
    [Header("UI (single image)")]
    [Tooltip("The Image GameObject in your Canvas (your PNG).")]
    public GameObject promptImage;          

    [Header("Options")]
    public KeyCode depositKey = KeyCode.E;  
    public bool onlyShowIfHolding = true;   //only pops if nurse holds items

    [Header("Stats (optional)")]
    public int totalDeposited;

    private Inventory invInRange;
    private Button imageButton;

    void Awake() {
        if (promptImage != null) {
            promptImage.SetActive(false);

            imageButton = promptImage.GetComponent<Button>();
            if (imageButton == null) imageButton = promptImage.AddComponent<Button>();
            imageButton.transition = Selectable.Transition.None;

            imageButton.onClick.RemoveAllListeners();
            imageButton.onClick.AddListener(DepositAll);
        }
    }

    void OnDisable() {
        //prevents duplicate listeners if the object is toggled
        if (imageButton != null) imageButton.onClick.RemoveListener(DepositAll);
        if (promptImage != null) promptImage.SetActive(false);
        invInRange = null;
    }

    void Update() {
        if (!invInRange) return;

        //keeps visibility in sync while player stays in range
        RefreshVisibility();

        if (promptImage && promptImage.activeSelf && Input.GetKeyDown(depositKey))
            DepositAll();
    }

    void OnTriggerEnter2D(Collider2D other) {
        var inv = other.GetComponent<Inventory>();
        if (!inv) return;

        invInRange = inv;
        RefreshVisibility();
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Inventory>() != invInRange) return;

        invInRange = null;
        if (promptImage) promptImage.SetActive(false);
    }

    void RefreshVisibility() {
        if (!promptImage) return;
        bool canShow = invInRange != null && (!onlyShowIfHolding || CountItems(invInRange) > 0);
        if (promptImage.activeSelf != canShow)
            promptImage.SetActive(canShow);
    }

    int CountItems(Inventory inv) {
        if (inv == null || inv.isFull == null) return 0;
        int c = 0;
        for (int i = 0; i < inv.isFull.Length; i++)
            if (inv.isFull[i]) c++;
        return c;
    }

    int ClearAll(Inventory inv) {
        if (inv == null || inv.slots == null || inv.isFull == null) return 0;
        int removed = 0;
        for (int i = 0; i < inv.slots.Length; i++) {
            if (!inv.isFull[i]) continue;
            Transform t = inv.slots[i].transform;
            for (int k = t.childCount - 1; k >= 0; k--)
                Object.Destroy(t.GetChild(k).gameObject);
            inv.isFull[i] = false;
            removed++;
        }
        return removed;
    }

    void DepositAll() {
        if (!invInRange) return;

        int deposited = ClearAll(invInRange);
        if (deposited <= 0) return;

        totalDeposited += deposited;

        if (promptImage) promptImage.SetActive(false);

    }
}
