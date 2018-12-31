using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    public float ScreenTime;
    public float CleanlinessPerSecond, BathTime;
    public float MoveSpeed;
    public Vector2 DirectionSwitchTimeRange;
    public Vector2 MoveTimeRange;
    public float GrabDelay, FallSpeed, FloorDelay, RiseSpeed;
    public float FloatHeight, FloorHeight;
    public Transform BathTarget;
    public float CarrySpeed, PlayerCarryYOffset, BathWashMagnitude, BathWashScale;
    public MonsterMovement PlayerRef;

    bool moving, carrying;
    Vector2 moveDir;

    IEnumerator mainEnum;
    float screenTimer;
    Rigidbody2D rb;

    Vector2 BathLocation => BathTarget.position;

    void Start ()
    {
        StartCoroutine(mainEnum = mainRoutine());
        rb = GetComponent<Rigidbody2D>();
        screenTimer = ScreenTime;
    }

    void Update ()
    {
        screenTimer -= Time.deltaTime;
        if (screenTimer <= 0)
        {
            SceneManager.LoadScene("LivingRoom");
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (moving)
        {
            moveDir *= -1;
        }
        else if (!carrying && other.gameObject.tag == "Player")
        {
            StopCoroutine(mainEnum);
            StartCoroutine(carryRoutine());
        }
    }

    IEnumerator mainRoutine ()
    {
        while (true)
        {
            yield return moveRoutine();
            yield return grabRoutine();
        }
    }

    IEnumerator moveRoutine ()
    {
        moving = true;
        moveDir = RandomExtra.Chance(.5f) ? Vector2.right : Vector2.left;

        float mainTimer = Random.Range(MoveTimeRange.x, MoveTimeRange.y);
        float directionTimer = Random.Range(DirectionSwitchTimeRange.x, DirectionSwitchTimeRange.y);

        while (mainTimer > 0)
        {
            mainTimer -= Time.deltaTime;
            directionTimer -= Time.deltaTime;

            if (directionTimer <= 0)
            {
                directionTimer = Random.Range(DirectionSwitchTimeRange.x, DirectionSwitchTimeRange.y);
                moveDir *= -1;
            }

            transform.position += (Vector3) moveDir * MoveSpeed * Time.deltaTime;

            yield return null;
        }

        moving = false;
    }

    IEnumerator grabRoutine ()
    {
        yield return new WaitForSeconds(GrabDelay);
        while (transform.position.y > FloorHeight)
        {
            transform.position += Vector3.down * FallSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, FloorHeight);

        yield return new WaitForSeconds(FloorDelay);
        while (transform.position.y < FloatHeight)
        {
            transform.position += Vector3.up * RiseSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, FloatHeight);
    }

    IEnumerator carryRoutine ()
    {
        carrying = true;
        PlayerRef.SetActive(false);
        rb.simulated = false;

        PlayerRef.transform.parent = transform;
        PlayerRef.transform.localPosition = new Vector2(0, PlayerCarryYOffset);
        
        Vector2 oldLocation = new Vector2(transform.position.x, FloatHeight);

        // move to bath
        while (transform.position.y < BathLocation.y)
        {
            transform.position += Vector3.up * CarrySpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector2(transform.position.x, BathLocation.y);
        
        while (transform.position.x < BathLocation.x)
        {
            transform.position += Vector3.right * CarrySpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = BathLocation;

        // wash
        float bathTimer = BathTime;
        while (bathTimer > 0)
        {
            bathTimer -= Time.deltaTime;
            var offset = Mathf.Sin((BathTime - bathTimer) * BathWashScale) * BathWashMagnitude;
            transform.position = BathLocation + Vector2.up * offset;

            ScoreManager.Instance.Dirtiness -= CleanlinessPerSecond * Time.deltaTime;
            
            yield return null;
        }

        // return to previous location
        while (Vector2.Distance(transform.position, oldLocation) > 0.1f)
        {
            transform.position += ((Vector3) oldLocation - transform.position).normalized * CarrySpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = oldLocation;

        PlayerRef.SetActive(true);
        PlayerRef.transform.parent = null;

        yield return new WaitForSeconds(2);

        rb.simulated = true;
        carrying = false;
        StartCoroutine(mainEnum = mainRoutine());
    }
}
