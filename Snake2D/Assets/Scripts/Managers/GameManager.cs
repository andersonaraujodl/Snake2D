using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public UIController uiController;

    public Snake snake;
    public Food food;

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
        snakeHeadObj = Instantiate(uiController.snakeHeadPrefab, uiController.level.transform);
        snake = snakeHeadObj.GetComponent<Snake>();
        snake.Init();
    }

    public void SpawnFood()
    {
        foodObj = Instantiate(uiController.foodPrefab, uiController.level.transform);
        food = foodObj.GetComponent<Food>();
        Vector2 randomPos = new Vector2(Random.Range(-uiController.GetWidth, uiController.GetWidth), Random.Range(-uiController.GetHeight, uiController.GetHeight));

        //The following code rounds the position to a multiple of the ScaleMultiplier, ensuring the food stays in regular position, in case the scale of the snake != 1
        float roundedX = Mathf.Round(randomPos.x / uiController.GetScaleMultiplier) * uiController.GetScaleMultiplier;
        float roundedY = Mathf.Round(randomPos.y / uiController.GetScaleMultiplier) * uiController.GetScaleMultiplier;
        randomPos = new Vector2(roundedX, roundedY);

        foodObj.transform.position = randomPos;
        food.Init(randomPos);
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