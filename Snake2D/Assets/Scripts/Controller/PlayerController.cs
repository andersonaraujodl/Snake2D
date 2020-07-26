using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED_LOWEST_VALUE = 2;
    private const float SPEED_DECREMENT = 2;

    private Snake snake;
    private float currentTimeCount;
    private bool canChangeDir = true; //Created To Avoid player to change direction before the interval of movement
    private float intervalBetweenMove = 50f;

    public void Init(Snake _snake)
    {
        intervalBetweenMove = 50f;
        currentTimeCount = intervalBetweenMove / 100;
        snake = GetComponent<Snake>();
    }

    void Update()
    {
        if(GameManager.Instance.isRunningGame)
        {
            if(canChangeDir)
            {
                CheckInput();
            }

            currentTimeCount -= Time.deltaTime;

            if (currentTimeCount <= 0)
            {
                snake.Move();
                currentTimeCount = intervalBetweenMove / 100;
                canChangeDir = true;
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (snake.Direction.y != -1f)
            {
                canChangeDir = false;
                snake.Direction = new Vector2(0, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (snake.Direction.y != 1f)
            {
                canChangeDir = false;
                snake.Direction = new Vector2(0, -1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (snake.Direction.x != 1f)
            {
                canChangeDir = false;
                snake.Direction = new Vector2(-1f, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (snake.Direction.x != -1f)
            {
                canChangeDir = false;
                snake.Direction = new Vector2(1f, 0);
            }
        }
    }

    public void IncreaseSpreed()
    {
        if (intervalBetweenMove > SPEED_LOWEST_VALUE)
        {
            intervalBetweenMove -= SPEED_DECREMENT;
        }
    }
}