using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public UIController uiController;

    public Snake snake;
    public Food food;
    public PlayerController playerController;

    private int goldenFoodCounter;
    private int regularFoodCounter;
    private int score;

    private GameObject snakeHeadObj;
    private GameObject foodObj;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        Init();
    }

    private void Init()
    {
        score = 0;
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
            food.Respawn();
        }
    }

    public void SetScore(bool isGoldenFood = false)
    {
        if(isGoldenFood)
        {
            score += 500;
            ++goldenFoodCounter;
        }
        else
        {
            score += 100;
            ++regularFoodCounter;
        }

        //TODO Still need to print on UI

        /* Not Readable version:
            score += isGoldenFood ? 500 : 100;
            goldenFoodCounter += Convert.ToInt32(isGoldenFood);
            regularFoodCounter += Convert.ToInt32(!isGoldenFood);
        */
    }
}