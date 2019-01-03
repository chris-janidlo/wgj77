using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using crass;

public class LivingRoomSwitcher : MonoBehaviour
{
    [Tooltip("Evaluated based on CoinCount every SecondsPerRoll")]
    public AnimationCurve CoinGameProbability;
    [Tooltip("Evaluated based on Dirtiness every SecondsPerRoll")]
    public AnimationCurve WashGameProbability;
    public float SecondsPerRoll;
    [Tooltip("Will never switch within Cooldown seconds of returning to living room")]
    public float Cooldown;

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(Cooldown);

        bool order = false;

        while (true)
        {
            bool coinChance = RandomExtra.Chance(CoinGameProbability.Evaluate(ScoreManager.Instance.CoinCount));
            bool washChance = RandomExtra.Chance(WashGameProbability.Evaluate(ScoreManager.Instance.Dirtiness));

            // might be unnecessary, but we'll switch which game we check first every time so we don't favor one over the other
            if (order = !order)
            {
                if (coinChance)
                {
                    SceneManager.LoadScene("Coin");
                }
                else if (washChance)
                {
                    SceneManager.LoadScene("Bath");
                }
            }
            else
            {
                if (washChance)
                {
                    SceneManager.LoadScene("Bath");
                }
                else if (coinChance)
                {
                    SceneManager.LoadScene("Coin");
                }
            }

            yield return new WaitForSeconds(SecondsPerRoll);
        }
    }
}
