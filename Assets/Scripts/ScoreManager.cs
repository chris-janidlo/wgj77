using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class ScoreManager : Singleton<ScoreManager>
{
    public bool IsRunning;

    public float Hunger, Dirtiness, Frustration;
    public int CoinCount;

    public float HungerRate, DirtinessRate, FrustrationRate;

    void Awake ()
    {
        if (SingletonGetInstance() == null)
        {
            SingletonSetInstance(this, false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // this allows different scenes to set different rates, by setting different rates on the ScoreManager per scene
            Instance.HungerRate = HungerRate;
            Instance.DirtinessRate = DirtinessRate;
            Instance.FrustrationRate = FrustrationRate;
            Destroy(gameObject);
        }
    }

    void Start ()
    {
        Initialize();
    }

    void Update ()
    {
        if (!IsRunning) return;
        
        Hunger += HungerRate * Time.deltaTime;
        Hunger = Mathf.Clamp(Hunger, 0, 100);

        Dirtiness += DirtinessRate * Time.deltaTime;
        Dirtiness = Mathf.Clamp(Dirtiness, 0, 100);

        if (Hunger == 100 && Dirtiness == 100)
        {
            Debug.Log("you win");
            IsRunning = false;
        }

        Frustration -= FrustrationRate * Time.deltaTime;
        Frustration = Mathf.Clamp(Frustration, 0, 100);
    }

    public void Initialize ()
    {
        Hunger = 0;
        Dirtiness = 0;
        Frustration = 0;
        CoinCount = 0;
    }
}
