using UnityEngine;

public class CommunicationBoardUI : MonoBehaviour {
    [Header("Wiring")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform wordTray;
    [SerializeField] private Transform boardArea;
    [SerializeField] private DraggableWord wordPrefab;

    [Header("Words to use")]
    [TextArea]
    public string[] words =
    {
        "AIDET",
        "Plan of care",
        "Toileting schedule",
        "Rounding schedule",
        "4Ps",
        "Mobilization equipment",
        "Name",
        "Phone number"
    };

    private bool built;

    private void Awake() {
        BuildIfNeeded();
        Show(false);
    }

    public void Show(bool on) {
        if (!built) BuildIfNeeded();
        panel.SetActive(on);
    }

    private void BuildIfNeeded() {
        if (built) return;
        foreach (var w in words) {
            var item = Instantiate(wordPrefab, wordTray);
            item.SetText(w);
        }
        built = true;
    }
}
