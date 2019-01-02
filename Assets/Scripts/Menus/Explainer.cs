using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explainer : MonoBehaviour
{
    public float MinOnScreenTime, MaxOnScreenTime;

    bool skippable;

    void Update ()
    {
        if (skippable && Input.anyKey)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Start ()
    {
        yield return new WaitForSecondsRealtime(MinOnScreenTime);
        skippable = true;
        yield return new WaitForSecondsRealtime(MaxOnScreenTime - MinOnScreenTime);
        Destroy(gameObject);
    }
}
