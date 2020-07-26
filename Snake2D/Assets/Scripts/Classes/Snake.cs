using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public Color headColor;
    public Color bodyColor;

    private int size;
    private Vector2 direction;

    private int verticalLimit;
    private int horizontalLimit;

    public List<Transform> bodyList = new List<Transform>();

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public void Init()
    {
        size = transform.childCount - 1;
        transform.position = Vector2.zero;
        direction = new Vector2(0, 1f);

        verticalLimit = GameManager.Instance.uiController.Height;
        horizontalLimit = GameManager.Instance.uiController.Width;

        for (int i = 0; i <= size; ++i)
        {
            Transform bodyPieceObj = transform.GetChild(i);
            bodyList.Add(bodyPieceObj);

            SnakeBody bodyPieceScript = bodyPieceObj.GetComponent<SnakeBody>();
            bodyPieceScript.Init(this);
        }
    }

    public void Move()
    {
        Vector2 movePosition = (Vector2)head.position + direction;

        tail.transform.position = movePosition;
        head.GetComponent<SnakeBody>().IsHead(false);
        head = tail;
        head.GetComponent<SnakeBody>().IsHead(true);

        bodyList.Insert(0, tail);
        bodyList.RemoveAt(bodyList.Count - 1);
        tail = bodyList[bodyList.Count - 1];

        CheckCollision();
    }

    public void CheckCollision()
    {
        if (head.position == GameManager.Instance.food.transform.position)
        {
            //Hit Food
            SoundManager.Instance.PlaySFX(SoundManager.Instance.GetSFXClip("Eat"));
            Grow();
            GameManager.Instance.SetScore();
        }
        else if (head.position.y > verticalLimit || head.position.y < -verticalLimit ||
            head.position.x > horizontalLimit || head.position.x < -horizontalLimit)
        {
            //Hit Wall
            GameManager.Instance.GameOver();
        }
        else if(GameManager.Instance.CollideWithBody(head.position, true))
        {
            //Hit own body
            GameManager.Instance.GameOver();
        }
    }

    public void Grow()
    {
        Size++;
        GameObject bodyPiece = Instantiate(GameManager.Instance.uiController.snakeBodyPrefab, head.position, Quaternion.identity, GameManager.Instance.snake.gameObject.transform);
        bodyPiece.GetComponent<SnakeBody>().Init(this);
        head = bodyPiece.transform; //Make Head to ensure the snake only grows once the tail passes through the position of the food, avoiding possible collisions when the player reachers higher snake sizes
        bodyList.Insert(0, head);
    }
}