using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FallingObject : MonoBehaviour
{
    public int CoinValue;

    public float FallSpeed;

    void Update ()
    {
        transform.position += Vector3.down * FallSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        switch (other.tag)
        {
            case "FallingObject":
            case "Player":
                break;

            case "Basket":
                ScoreManager.Instance.CoinCount += CoinValue;
                CoinManager.Instance.BasketCount -= 1;
                if (CoinValue > 0)
                {
                    ScoreManager.Instance.Frustration = 0;
                }
                goto default;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
