using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button restartButton;
    public CanvasGroup canvasGroup;

    private UIController uiController;

    public void Init(UIController _uiController)
    {
        gameObject.SetActive(true);
        canvasGroup = GetComponent<CanvasGroup>();
        uiController = _uiController;

        uiController.FadeCanvasGroup(canvasGroup, 1, 0.5f);

        restartButton.onClick.AddListener(() =>
        {
            uiController.FadeCanvasGroup(canvasGroup, 0, 0.5f);
            GameManager.Instance.Init();
        });
    }

    
}
