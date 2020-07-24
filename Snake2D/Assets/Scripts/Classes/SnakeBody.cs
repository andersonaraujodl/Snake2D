using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public bool isHead = false;

    public Color headColor;
    public Color bodyColor;

    public SpriteRenderer bodySprite;

    public bool IsHead
    {
        get { return isHead; }
        set
        {
            isHead = value;
            bodySprite.color = isHead ? headColor : bodyColor;
            bodySprite.sortingOrder = 1;
        }
    }

    public void Init()
    {
        bodySprite = GetComponent<SpriteRenderer>();
    }
}
