using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Fruit : MonoBehaviour
{
    public float LifeTime;
    public float LifeTimer;
    public float HungerScore;

    SpriteRenderer sr;

    void Start ()
    {
        LifeTimer = LifeTime;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        LifeTimer -= Time.deltaTime;
        sr.color = Color.Lerp(Palette.Colors[PaletteIdentifier.On], Color.white, LifeTimer / LifeTime);

        if (LifeTimer <= 0)
        {
            die();
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.Instance.Hunger -= HungerScore;
            die();
        }
    }

    void die ()
    {
        Destroy(gameObject);
    }
}
