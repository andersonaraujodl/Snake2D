using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    private Vector2 position;
    private bool isGolden;
    private float secondsToVanish = 25f;

    public void Init(Vector2 _position)
    {
        position = _position;
        StartCoroutine("Countdown");
    }

    public Vector2 GetPosition
    {
        get { return position; }
    }

    public void Respawn()
    {
        Vector2 randomPos;

        do
        {
            randomPos = new Vector2(Random.Range(-GameManager.Instance.uiController.GetWidth, GameManager.Instance.uiController.GetWidth), Random.Range(-GameManager.Instance.uiController.GetHeight, GameManager.Instance.uiController.GetHeight));
            
            //The following code rounds the position to a multiple of the ScaleMultiplier, ensuring the food stays in regular position, in case the scale of the snake != 1
            float roundedX = Mathf.Round(randomPos.x / GameManager.Instance.uiController.GetScaleMultiplier) * GameManager.Instance.uiController.GetScaleMultiplier;
            float roundedY = Mathf.Round(randomPos.y / GameManager.Instance.uiController.GetScaleMultiplier) * GameManager.Instance.uiController.GetScaleMultiplier;
            randomPos = new Vector2(roundedX, roundedY);
        } while (CheckSpawnPos(randomPos));

        gameObject.transform.position = randomPos;
    }

    private bool CheckSpawnPos(Vector2 randomPos)
    {
        for(int i = 0; i < GameManager.Instance.snake.bodyList.Count; ++i)
        {
            if ((Vector2) GameManager.Instance.snake.bodyList[i].position == randomPos)
                return true;
        }

        return false;
    }

    public IEnumerator Countdown()
    {
        float counter = secondsToVanish;

        while (counter > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            counter -= 1;
        }

        Respawn();
        yield break;
    }
}
