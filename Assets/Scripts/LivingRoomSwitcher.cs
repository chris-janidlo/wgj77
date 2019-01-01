using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using crass;

public class LivingRoomSwitcher : MonoBehaviour
{
    [Tooltip("If the number of coins is less than this, there's a chance we play the coin game")]
    public float CoinGameThreshold;
    public RepeatedProbability CoinGameProbability;
    [Tooltip("If the dirtiness level is higher than this, there's a chance we play the wash game")]
    public float WashGameThreshold;
    public RepeatedProbability WashGameProbability;
    [Tooltip("Will never switch within Cooldown seconds of returning to living room")]
    public float Cooldown;

    float timer;

    void Start ()
    {
        CoinGameProbability.Name = "coin";
        WashGameProbability.Name = "wash";

        StartCoroutine(CoinGameProbability.CheckRoutine(startNewGame));
        StartCoroutine(WashGameProbability.CheckRoutine(startNewGame));

        timer = Cooldown;
    }

    void Update ()
    {
        timer -= Time.deltaTime;
    }

    void startNewGame (string gameName)
    {
        if (timer > 0) return;
        
        switch (gameName)
        {
            case "coin":
                if (ScoreManager.Instance.CoinCount >= CoinGameThreshold) return;
                SceneManager.LoadScene("Coin");
                break;
            case "wash":
                if (ScoreManager.Instance.Dirtiness <= WashGameThreshold) return;
                SceneManager.LoadScene("Bath");
                break;
        }
        StopAllCoroutines();
    }
}

[Serializable]
public class RepeatedProbability
{
    public float SecondsPerRoll, ChancePerRoll;
    public string Name;

    public IEnumerator CheckRoutine (Action<string> callback)
    {
        while (true)
        {
            yield return new WaitForSeconds(SecondsPerRoll);
            if (RandomExtra.Chance(ChancePerRoll))
            {
                callback(Name);
            }
        }
    }
}
