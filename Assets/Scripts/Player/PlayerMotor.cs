using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4f;
    public float jumpForce = 6f;

    private Rigidbody rb;
    private Vector3 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(float xInput)
    {
        Vector3 vel = rb.linearVelocity;
        vel.x = xInput * moveSpeed;
        rb.linearVelocity = vel;

        // mirar hacia la dirección del movimiento
        if (xInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(xInput), 1, 1);
        }
    }

    public void ApplyJump()
    {
        Vector3 vel = rb.linearVelocity;
        vel.y = jumpForce;
        rb.linearVelocity = vel;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f,
                               Vector3.down,
                               0.2f);
    }

    public float VerticalVelocity()
    {
        return rb.linearVelocity.y;
    }
}
