using System.Collections;
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

    public Transform Tail
    {
        get { return tail; }
        set { tail = value; }
    }

    public void Init()
    {
        size = transform.childCount - 1;
        transform.position = Vector2.zero;
        direction = new Vector2(0, 1f);
        bodyList.Add(head);

        for (int i = 1; i <= size; ++i)
        {
            Transform bodyPieceObj = transform.GetChild(i);
            bodyList.Add(bodyPieceObj);

            SnakeBody bodyPieceScript = bodyPieceObj.GetComponent<SnakeBody>();
            bodyPieceScript.Init();
        }
    }

    public void Move()
    {
        Vector2 movePosition = (Vector2) head.position + direction * GameManager.Instance.uiController.GetScaleMultiplier;

        tail.transform.position = movePosition;
        head.GetComponent<SnakeBody>().IsHead(false);
        head = tail;
        head.GetComponent<SnakeBody>().IsHead(true);

        bodyList.Insert(0, tail);
        bodyList.RemoveAt(bodyList.Count - 1);
        tail = bodyList[bodyList.Count - 1];

        CheckCollition();
    }

    //TODO Maybe I should think about a CollisionManager
    public void CheckCollition()
    {
        //TODO I should also change to collider to avoid miscalculation between Vector3.
        if (head.position == GameManager.Instance.food.transform.position)
        {
            //TODO IncreaseScore
            //TODO CheckSpeed
            //TODO CheckGolden

            Grow();
            GameManager.Instance.food.Respawn();
        }
    }

    public void Grow()
    {
        Size++;
        GameObject bodyPiece = Instantiate(GameManager.Instance.uiController.snakeBodyPrefab, head.position, Quaternion.identity, GameManager.Instance.snake.gameObject.transform);
        bodyPiece.GetComponent<SnakeBody>().Init();
        head = bodyPiece.transform; //Make Head to ensure the snake only grows once the tail passes through the position of the food, avoiding possible collisions when the player reachers higher snake sizes
        bodyList.Insert(0, head);
    }
}