using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explainer : MonoBehaviour
{
    static bool skippable; // static so the player only *has* to sit through the screen once per time the game is loaded

    public float MinOnScreenTime, MaxOnScreenTime;

    bool keyHasBeenUp;

    void Update ()
    {
        if (!Input.anyKey)
        {
            keyHasBeenUp = true;
        }

        if (skippable && keyHasBeenUp && Input.anyKey)
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
