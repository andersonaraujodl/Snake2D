using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private const float SCALE_MULTIPLIER = 2; //Used in case the default scale of the snake head is changed - it would be better working with textures of propper sizes
    
    private Vector2 position;
    private Vector2 direction;
    private float intervalBetweenMove = 1.2f;
    private float currentTimeCount;

    public void Init()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(0, 1f);
        currentTimeCount = intervalBetweenMove;
    }

    private void Update()
    {
        CheckInput();
        currentTimeCount -= Time.deltaTime;

        if (currentTimeCount <= 0)
        {
            Move();
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction.y != -1f)
            {
                direction = new Vector2(0, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction.y != 1f)
            {
                direction = new Vector2(0, -1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction.x != 1f)
            {
                direction = new Vector2(-1f, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction.x != -1f)
            {
                direction = new Vector2(1f, 0);
            }
        }
    }

    private void Move()
    {
        transform.Translate(direction * SCALE_MULTIPLIER);
        currentTimeCount = intervalBetweenMove;
    }
}
