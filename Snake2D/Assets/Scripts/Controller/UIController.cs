using System.Collections;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("General")]
    public TextMeshProUGUI scoreValueText;
    public TextMeshProUGUI highScoreValueText;
    public SpriteRenderer background;

    [Header("Prefabs")]
    public GameObject snakePrefab;
    public GameObject snakeBodyPrefab;
    public GameObject foodPrefab;

    [Header("Screens")]
    public GameObject level;
    public Menu menuScreen;
    public GameOver gameOverScreen;

    private int width;
    private int height;

    public int Width
    {
        get { return width;  }
    }

    public int Height
    {
        get { return height; }
    }

    public void Init()
    {
        width = (int)background.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = (int)background.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        gameOverScreen.gameObject.SetActive(false);
        menuScreen.Init(this);
    }

    public void DrawScore(int score)
    {
        scoreValueText.text = score.ToString("D8");
    }

    public void DrawHighScore(int highScore)
    {
        highScoreValueText.text = highScore.ToString("D8");

    }

    public void FadeCanvasGroup(CanvasGroup canvasGroup, int alpha, float duration)
    {
        if(!canvasGroup.gameObject.activeSelf)
        {
            canvasGroup.gameObject.SetActive(true);
        }

        StartCoroutine(FadeInOut(canvasGroup, alpha, duration));
    }

    private IEnumerator FadeInOut(CanvasGroup canvasGroup, int alpha, float duration)
    {
        float elapsedTime = 0;
        float startValue = canvasGroup.alpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, alpha, elapsedTime / duration);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }

        if (canvasGroup.alpha > 0)
        {
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.gameObject.SetActive(false);
        }
    }
}