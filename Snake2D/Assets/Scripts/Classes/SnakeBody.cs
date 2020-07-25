using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public bool isHead = false;

    public Color headColor;
    public Color bodyColor;

    public SpriteRenderer bodySprite;

    public void Init()
    {
        bodySprite = GetComponent<SpriteRenderer>();
    }

    public void IsHead(bool isHead)
    {
        bodySprite.color = isHead ? headColor : bodyColor;
        bodySprite.sortingOrder = 1;
    }
}