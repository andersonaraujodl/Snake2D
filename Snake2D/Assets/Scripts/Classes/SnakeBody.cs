using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public bool isHead = false;
    public SpriteRenderer bodySprite;

    private Snake snake;

    public void Init(Snake _snake)
    {
        snake = _snake;
        bodySprite = GetComponent<SpriteRenderer>();
    }

    public void IsHead(bool isHead)
    {
        bodySprite.color = isHead ? snake.headColor : snake.bodyColor;
        bodySprite.sortingOrder = 1;
    }
}