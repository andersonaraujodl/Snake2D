using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public bool isRunningGame = false;
    [HideInInspector] public Food food;
    [HideInInspector] public Snake snake;
    [HideInInspector] public UIController uiController;
    [HideInInspector] public PlayerController playerController;

    private int goldenFoodCounter;
    private int regularFoodCounter;
    private int totalFoodCounter;
    private int score;

    private GameObject snakeHeadObj;
    private GameObject foodObj;

    public int RegularFoodCounter
    {
        get { return regularFoodCounter; }
    }

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        Init();
    }

    public void Init()
    {
        score = 0;
        totalFoodCounter = 0;
        goldenFoodCounter = 0;
        regularFoodCounter = 0;
        isRunningGame = true;

        uiController.Init();
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

        //Increment speed every 3 foods
        if (totalFoodCounter % 3 == 0)
        {
            playerController.IncreaseSpreed();
        }

        food.Respawn();

        //TODO Still need to print on UI
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

        StartCoroutine("DestroySnake");
    }

    private IEnumerator DestroySnake()
    {
        foreach (var bodyPiece in snake.bodyList)
        {
            Destroy(bodyPiece.gameObject);
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(snake.gameObject);
        Destroy(food.gameObject);

        uiController.gameOverScreen.Init(uiController);
        yield break;
    }
}