using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject level;
    public GameObject snakePrefab;
    public GameObject snakeBodyPrefab;
    public GameObject foodPrefab;
    public SpriteRenderer background;

    private const float SCALE_MULTIPLIER = 2; //Used in case the default scale of the snake head is changed - it would be better working with textures of propper sizes

    private float width;
    private float height;

    public float GetWidth
    {
        get { return width;  }
    }

    public float GetHeight
    {
        get { return height; }
    }

    public float GetScaleMultiplier
    {
        get { return SCALE_MULTIPLIER; }
    }

    public void Init()
    {
        width = background.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = background.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }
}