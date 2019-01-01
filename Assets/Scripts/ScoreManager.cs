using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using crass;

public class ScoreManager : Singleton<ScoreManager>
{
    public static readonly List<string> DisabledSceneNames = new List<string>() { "Menu", "Victory" };

    public float Hunger, Dirtiness, Frustration;
    public float CoinCount;

    public float HungerRate, DirtinessRate, FrustrationRate;

    public float LifeTime;

    bool initialized;

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
        if (DisabledSceneNames.Contains(SceneManager.GetActiveScene().name))
        {
            initialized = false;
            return;
        }

        if (!initialized)
        {
            Initialize();
        }
        
        LifeTime += Time.deltaTime;

        Hunger += HungerRate * Time.deltaTime;
        Hunger = Mathf.Clamp(Hunger, 0, 100);

        Dirtiness += DirtinessRate * Time.deltaTime;
        Dirtiness = Mathf.Clamp(Dirtiness, 0, 100);

        if (Hunger == 100 && Dirtiness == 100)
        {
            SceneManager.LoadScene("Victory");
        }

        Frustration += FrustrationRate * Time.deltaTime;
        Frustration = Mathf.Clamp(Frustration, 0, 100);
    }

    public void Initialize ()
    {
        initialized = true;
        
        Hunger = 0;
        Dirtiness = 0;
        Frustration = 0;
        CoinCount = 37;
        LifeTime = 0;
    }
}
