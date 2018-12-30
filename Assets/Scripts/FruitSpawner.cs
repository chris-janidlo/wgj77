using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class FruitSpawner : MonoBehaviour
{
    public Bounds SpawnBounds;
    public List<Bounds> ForbiddenBounds;
    public float MinDistFromPlayer, MaxDistFromPlayer;
    public Vector2 SpawnTimeRange;
    public FruitBag FruitPrefabs;
    public MonsterMovement PlayerRef;

    float timer;

    void Start ()
    {
        timer = Random.Range(SpawnTimeRange.x, SpawnTimeRange.y);
    }

    void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(SpawnTimeRange.x, SpawnTimeRange.y);
            StartCoroutine(spawnFruit());
        }
    }

    IEnumerator spawnFruit ()
    {
        Vector2 spawnPos;
        float spawnDist;
        int tryCounter = 0;
        do
        {
            spawnPos = SpawnBounds.GetRandom();
            spawnDist = Vector2.Distance(spawnPos, PlayerRef.transform.position);
            if (tryCounter++ % 10 == 0) yield return null;
        }
        while
        (
            ForbiddenBounds.Any(b => b.PointInside(spawnPos)) ||
            spawnDist < MinDistFromPlayer ||
            spawnDist > MaxDistFromPlayer
        );
        yield return null; // TODO: not sure if we need a *guaranteed* yield return for Unity to do coroutines, or if *any* yield return works

        float zAngle = Random.Range(0, 360);
        Instantiate(FruitPrefabs.GetNext(), spawnPos, Quaternion.Euler(0, 0, zAngle));
    }
}

[System.Serializable]
public class Bounds
{
    public Vector2 LowerLeft, UpperRight;

    public Vector2 GetRandom ()
    {
        float x = Random.Range(LowerLeft.x, UpperRight.x);
        float y = Random.Range(LowerLeft.y, UpperRight.y);
        return new Vector2(x, y);
    }

    public bool PointInside (Vector2 point)
    {
        return
            point.x >= LowerLeft.x && point.x <= UpperRight.x &&
            point.y >= LowerLeft.y && point.y <= UpperRight.y;
    }
}

[System.Serializable]
public class FruitBag : BagRandomizer<Fruit> {}
