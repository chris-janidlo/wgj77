using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MonsterMovement : MonoBehaviour
{
    public HorizontalMovement Ground, Air;
    public bool JumpEnabled;
    public AnimationCurve JumpYVelocity;

    public float HalfHeight, GroundedFudge;

    public LayerMask GroundLayers;

    Rigidbody2D rb;
    float jumpTimer;
    IEnumerator jumpEnum;
    bool active = true;

    Animator animator;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Ground.ToMove = rb;
        Air.ToMove = rb;

        animator = GetComponent<Animator>();
    }

    void Update ()
    {
        if (!active) return;

        float input = Input.GetAxisRaw("Horizontal");
        if (isGrounded())
        {
            Ground.Move(input);
            if (JumpEnabled && jumpTimer <= 0 && Input.GetButton("Vertical"))
            {
                StartCoroutine(jumpEnum = jumpRoutine());
            }
        }
        else
        {
            Air.Move(input);
        }

        animator.SetBool("Walking", input != 0);
    }

    public void SetActive (bool value)
    {
        active = value;
        if (!value)
        {
            rb.velocity = Vector2.zero;
            if (jumpTimer > 0 ) StopCoroutine(jumpEnum);
        }
        rb.simulated = value;
    }

    bool isGrounded ()
    {
        bool result = Physics2D.CircleCast(transform.position, GroundedFudge, Vector2.down, HalfHeight, GroundLayers);
        return result;
    }

    IEnumerator jumpRoutine ()
    {
        var keys = JumpYVelocity.keys;
        float maxTime = keys[keys.Length - 1].time;

        jumpTimer = maxTime;
        while (jumpTimer > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpYVelocity.Evaluate(maxTime - jumpTimer));
            jumpTimer -= Time.deltaTime;
            yield return null;
        }
        rb.velocity = new Vector2(rb.velocity.x, keys[keys.Length - 1].value);
    }

    [System.Serializable]
    public class HorizontalMovement
    {
        public Rigidbody2D ToMove { get; set; }

        public float TopSpeed, Acceleration;

        float decelSign;

        public void Move (float input)
        {
            float speed = ToMove.velocity.x;

            if (input == 0)
            {
                speed -= Time.deltaTime * Acceleration * decelSign;
                if (speed * decelSign <= 0) speed = 0;
            }
            else
            {
                speed += Time.deltaTime * Acceleration * Mathf.Sign(input);
                speed = Mathf.Clamp(speed, -TopSpeed, TopSpeed);
                decelSign = Mathf.Sign(speed);
            }

            ToMove.velocity = new Vector2(speed, ToMove.velocity.y);
        }
    }
}
