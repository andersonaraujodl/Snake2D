using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    private int size;
    private Vector2 direction;

    private List<Transform> bodyList = new List<Transform>();

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public int Size
    {
        get { return size; }
        set
        {
            size = value;
            Grow();
        }
    }

    public Transform SetTail
    {
        set
        {
            tail = value;
        }
    }

    public void Init()
    {
        tail = head;
        size = 2;
        transform.position = Vector2.zero;
        direction = new Vector2(0, 1f);
        bodyList.Add(head);

        for (int i = 1; i <= Size; ++i)
        {
            Grow();
        }
    }

    public void Move()
    {
        Vector2 movePosition = (Vector2) head.position + direction * GameManager.Instance.uiController.GetScaleMultiplier;

        tail.transform.position = movePosition;
        head.GetComponent<SnakeBody>().IsHead = false;
        head = tail;
        head.GetComponent<SnakeBody>().IsHead = true;

        bodyList.Insert(0, tail);
        bodyList.RemoveAt(bodyList.Count - 1);
        tail = bodyList[bodyList.Count - 1];

        CheckEat();
    }

    public void CheckEat()
    {
        if ((Vector2)head.position == GameManager.Instance.food.GetPosition)
        {
            //TODO IncreaseScore
            //TODO CheckSpeed
            //TODO CheckGolden

            Size++;
            GameManager.Instance.food.Respawn();
        }
    }

    public void Grow()
    {
        GameObject bodyPiece = Instantiate(GameManager.Instance.uiController.snakeBodyPrefab, head.position, Quaternion.identity, GameManager.Instance.uiController.level.transform);
        bodyPiece.GetComponent<SnakeBody>().Init();
        head = bodyPiece.transform; //Make Head to ensure the snake only grows once the tail passes through the position of the food, avoiding possible collisions when the player reachers higher snake sizes
        bodyList.Insert(0, head);
    }
}