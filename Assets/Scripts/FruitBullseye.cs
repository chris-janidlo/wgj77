using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBullseye : MonoBehaviour
{
    public float Delay;
    
    public Fruit ToSpawn { get; set; }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(Delay);

        float zAngle = Random.Range(0, 360);
        Instantiate(ToSpawn, transform.position, Quaternion.Euler(0, 0, zAngle));

        Destroy(gameObject);
    }
}
