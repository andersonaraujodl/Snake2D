using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Snake snake;
    private bool canChangeDir = true; //Created To Avoid player to change direction before the interval of movement
    private float intervalBetweenMove = .5f;
    private float currentTimeCount;

    public void Init(Snake _snake)
    {
        currentTimeCount = intervalBetweenMove;
        snake = GetComponent<Snake>();
    }

    void Update()
    {
        if(canChangeDir)
        {
            CheckInput();
        }

        currentTimeCount -= Time.deltaTime;

        if (currentTimeCount <= 0)
        {
            snake.Move();
            currentTimeCount = intervalBetweenMove;
            canChangeDir = true;
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (snake.Direction.y != -1f)
            {
                snake.Direction = new Vector2(0, 1f);
                canChangeDir = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (snake.Direction.y != 1f)
            {
                snake.Direction = new Vector2(0, -1f);
                canChangeDir = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (snake.Direction.x != 1f)
            {
                snake.Direction = new Vector2(-1f, 0);
                canChangeDir = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (snake.Direction.x != -1f)
            {
                snake.Direction = new Vector2(1f, 0);
                canChangeDir = false;
            }
        }
    }
}