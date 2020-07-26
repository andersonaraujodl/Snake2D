using System.Collections;
using System.Collections.Generic;
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

        startButton.onClick.AddListener(() =>
        {
            uiController.FadeCanvasGroup(canvasGroup, 0, 0.5f);
            GameManager.Instance.StartGame();
        });

        instructionsButton.onClick.AddListener(() =>
        {
            uiController.FadeCanvasGroup(instructionsCanvas, 1, 0.5f);
        });

        backButton.onClick.AddListener(() =>
        {
            uiController.FadeCanvasGroup(instructionsCanvas, 0, 0.5f);
        });
    }

    public void ShowMenu()
    {
        uiController.FadeCanvasGroup(canvasGroup, 1, 0.5f);
    }
}
