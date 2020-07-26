using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    [HideInInspector] public bool isGolden;

    public Color regularColor;
    public Color goldenColor;

    private float secondsToVanish = 15f;
    private SpriteRenderer spriteRenderer;

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Respawn();
    }

    public void Respawn()
    {
        if(GameManager.Instance.isRunningGame)
        {
            if (!isGolden && GameManager.Instance.RegularFoodCounter > 0 && GameManager.Instance.RegularFoodCounter % 5 == 0)
            {
                TurnGolden(true);
            }
            else
            {
                TurnGolden(false);
            }

            Vector2 randomPos;

            do
            {
                randomPos = new Vector2(Random.Range(-GameManager.Instance.uiController.Width, GameManager.Instance.uiController.Width), Random.Range(-GameManager.Instance.uiController.Height, GameManager.Instance.uiController.Height));

            } while (GameManager.Instance.CollideWithBody(randomPos));

            gameObject.transform.position = randomPos;
        }
    }

    private void TurnGolden(bool _isGolden)
    {
        isGolden = _isGolden;

        if(_isGolden)
        {
            StartCoroutine("Countdown");
            spriteRenderer.color = goldenColor;
        }
        else
        {
            StopCoroutine("Countdown");
            spriteRenderer.color = regularColor;
        }
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