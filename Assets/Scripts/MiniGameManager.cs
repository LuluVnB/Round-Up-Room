using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText;
    public GameObject completionPanel;
    public Button closeButton;
    public Button resetButton;

    public DraggableEmoji[] allEmojis;
    public CommunicationBoardTrigger boardTrigger;

    private int score = 0;
    private int correctPlacements = 0;
    private int totalEmojis = 5;

    void Start() {
        UpdateScoreUI();

        if (completionPanel != null) {
            completionPanel.SetActive(false);
        }

        if (closeButton != null) {
            closeButton.onClick.AddListener(CloseGame);
        }

        if (resetButton != null) {
            resetButton.onClick.AddListener(ResetGame);
        }
    }

    public void OnCorrectPlacement() {
        score += 10;
        correctPlacements++;
        UpdateScoreUI();

        // Check if game is complete
        if (correctPlacements >= totalEmojis) {
            GameComplete();
        }
    }

    void UpdateScoreUI() {
        if (scoreText != null) {
            scoreText.text = "Score: " + score;
        }
    }

    void GameComplete() {
        if (completionPanel != null) {
            completionPanel.SetActive(true);
        }
    }

    public void ResetGame() {
        score = 0;
        correctPlacements = 0;
        UpdateScoreUI();

        if (completionPanel != null) {
            completionPanel.SetActive(false);
        }

        // Reset all emojis to original positions
        foreach (DraggableEmoji emoji in allEmojis) {
            if (emoji != null) {
                emoji.ResetPosition();
            }
        }
    }

    void CloseGame() {
        ResetGame();

        if (boardTrigger != null) {
            boardTrigger.CloseMiniGame();
        }
    }
}