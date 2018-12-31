using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MonsterMovement : MonoBehaviour
{
    public float GroundSpeed, GroundAccel, AirSpeed, AirAccel, JumpBurst;

    public float HalfHeight, GroundedFudge;

    public LayerMask GroundLayers;

    Rigidbody2D rb;
    float gravityMem;
    bool jumped, active = true;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (!active) return;

        float input = Input.GetAxis("Horizontal");
        if (isGrounded() && !jumped)
        {
            move(input, GroundAccel, GroundSpeed);
            if (Input.GetButton("Vertical"))
            {
                jumped = true;
                rb.AddForce(Vector3.up * JumpBurst, ForceMode2D.Impulse);
            }
        }
        else
        {
            jumped = false;
            move(input, AirAccel, AirSpeed);
        }
    }

    public void SetActive (bool value)
    {
        active = value;
        if (!value) rb.velocity = Vector2.zero;
        rb.simulated = value;
    }

    bool isGrounded ()
    {
        var hit = Physics2D.CircleCast(transform.position, GroundedFudge, Vector2.down, HalfHeight, GroundLayers);
        return hit.collider != null;
    }
    
    void move (float input, float accel, float top)
    {
        if (rb.velocity.magnitude < top)
        {
            Vector2 vel;
            if (input == 0)
            {
                vel = Vector2.left * rb.velocity.normalized * 0.5f;
            }
            else {
                vel = Vector2.right * input;
            }
            rb.velocity += vel * accel * Time.deltaTime;
        }
    }
}
