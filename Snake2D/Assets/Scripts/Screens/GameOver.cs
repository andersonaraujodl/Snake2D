using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    
    private CanvasGroup canvasGroup;
    private UIController uiController;

    public void Init(UIController _uiController)
    {
        gameObject.SetActive(true);
        UpdateScore();
        canvasGroup = GetComponent<CanvasGroup>();
        uiController = _uiController;

        uiController.FadeCanvasGroup(canvasGroup, 1, 0.5f);
        AudioClip clip = SoundManager.Instance.GetSFXClip("Eat");

        restartButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(clip);
            uiController.FadeCanvasGroup(canvasGroup, 0, 0.5f);
            GameManager.Instance.StartGame();
        });

        menuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(clip);
            uiController.FadeCanvasGroup(canvasGroup, 0, 0.5f);
            uiController.DrawScore(0);
            uiController.menuScreen.ShowMenu();
        });
    }

    private void UpdateScore()
    {
        scoreText.text = string.Concat("Score: ", GameManager.Instance.Score.ToString());
        highScoreText.text = string.Concat("High Score: ", GameManager.Instance.HighScore.ToString());
    }
}