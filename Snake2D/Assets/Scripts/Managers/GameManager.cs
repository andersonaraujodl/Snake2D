using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public Food food;
    [HideInInspector] public Snake snake;
    [HideInInspector] public UIController uiController;
    [HideInInspector] public PlayerController playerController;

    private int score;
    private int highScore; 
    private int totalFoodCounter;
    private int goldenFoodCounter;
    private int regularFoodCounter;
    private bool isRunningGame = false;

    private GameObject snakeHeadObj;
    private GameObject foodObj;

    public bool IsRunningGame
    {
        get { return isRunningGame; }
    }

    public int RegularFoodCounter
    {
        get { return regularFoodCounter; }
    }

    public int Score
    {
        get { return score; }
    }

    public int HighScore
    {
        get { return highScore; }
    }

    //Game Start Method
    private void Awake()
    {
        uiController = GetComponent<UIController>();
        Init();
    }

    public void Init()
    {
        uiController.Init();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        uiController.DrawHighScore(highScore);
    }

    public void StartGame()
    {
        score = 0;
        totalFoodCounter = 0;
        goldenFoodCounter = 0;
        regularFoodCounter = 0;

        isRunningGame = true;
        uiController.DrawScore(score);
        SoundManager.Instance.PlayMusic(SoundManager.Instance.GetMusicClip("Game"));
        CreateSnake();
        SpawnFood();
    }

    private void CreateSnake()
    {
        snakeHeadObj = Instantiate(uiController.snakePrefab, uiController.level.transform);
        snake = snakeHeadObj.GetComponent<Snake>();
        snake.Init();

        playerController = snakeHeadObj.GetComponent<PlayerController>();
        playerController.Init(snake);
    }

    public void SpawnFood()
    {
        if(foodObj == null)
        {
            foodObj = Instantiate(uiController.foodPrefab, uiController.level.transform);
            food = foodObj.GetComponent<Food>();
            food.Init();
        }
    }

    public void SetScore()
    {
        ++totalFoodCounter;

        if (food.isGolden)
        {
            score += 500;
            ++goldenFoodCounter;
        }
        else
        {
            score += 100;
            ++regularFoodCounter;
        }

        //Increment speed every 2 foods
        if (totalFoodCounter % 2 == 0)
        {
            playerController.IncreaseSpreed();
        }

        uiController.DrawScore(score);
        food.Respawn();
    }

    public bool CollideWithBody(Vector2 position, bool ignoreHead = false)
    {
        int i = ignoreHead ? 1 : 0;

        for (; i < snake.bodyList.Count; ++i)
        {
            if ((Vector2)snake.bodyList[i].position == position)
                return true;
        }

        return false;
    }

    public void GameOver()
    {
        isRunningGame = false;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            uiController.DrawHighScore(highScore);
        }

        StartCoroutine("DestroySnake");
    }

    private IEnumerator DestroySnake()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.GetSFXClip("Die"));

        foreach (var bodyPiece in snake.bodyList)
        {
            Destroy(bodyPiece.gameObject);
            yield return new WaitForSeconds(0.008f);
        }

        Destroy(snake.gameObject);
        Destroy(food.gameObject);

        uiController.gameOverScreen.Init(uiController);
        yield break;
    }
}