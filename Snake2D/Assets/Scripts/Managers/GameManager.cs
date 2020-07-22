using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private UIController uiController;

    private GameObject snakeHead;
    private Snake snake;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        CreateSnake();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSnake()
    {
        snakeHead = Instantiate(uiController.snakeHead, uiController.level.transform);
        snake = snakeHead.GetComponent<Snake>();
        snake.Init();
    }
}
