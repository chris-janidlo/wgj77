using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBasket : MonoBehaviour
{
    public float IFrameTime;
    public MonsterMovement PlayerRef;

    public bool IsInvuln => iFrameTimer > 0;

    float iFrameTimer;

    void Update ()
    {
        iFrameTimer -= Time.deltaTime;
        transform.position = Vector2.Lerp(transform.position, PlayerRef.transform.position, 0.5f);
    }

    public void Hurt ()
    {
        iFrameTimer = IFrameTime;
    }
}
