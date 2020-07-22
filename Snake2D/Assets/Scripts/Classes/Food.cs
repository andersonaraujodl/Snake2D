using System;
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
        GameManager.Instance.SpawnFood();
        Destroy(this.gameObject);
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
