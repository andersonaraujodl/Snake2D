using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button startButton;
    public Button backButton;
    public Button instructionsButton;
    public CanvasGroup instructionsCanvas;

    private CanvasGroup canvasGroup;
    private UIController uiController;

    public void Init(UIController _uiController)
    {
        uiController = _uiController;
        canvasGroup = GetComponent<CanvasGroup>();
        AudioClip clip = SoundManager.Instance.GetSFXClip("Eat");

        if (instructionsCanvas.gameObject.activeSelf)
        {
            uiController.FadeCanvasGroup(instructionsCanvas, 0, 0.1f);
        }

        startButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(clip);
            uiController.FadeCanvasGroup(canvasGroup, 0, 0.5f);
            GameManager.Instance.StartGame();
        });

        instructionsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(clip);
            uiController.FadeCanvasGroup(instructionsCanvas, 1, 0.5f);
        });

        backButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(clip);
            uiController.FadeCanvasGroup(instructionsCanvas, 0, 0.5f);
        });

        ShowMenu();
    }

    public void ShowMenu()
    {
        uiController.FadeCanvasGroup(canvasGroup, 1, 0.5f);

        SoundManager.Instance.PlayMusic(SoundManager.Instance.GetMusicClip("Menu"));
    }
}