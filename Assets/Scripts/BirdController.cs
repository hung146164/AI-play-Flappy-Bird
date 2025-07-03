using UnityEngine;
using Unity.MLAgents;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private FlappyAgent agent;

    [Header("Movement")] 
    public float jumpForce = 10f;
    public float maxY = 11.32f;

    [Header("Rotation")] public float maxRotationUp = 30f;
    public float maxRotationDown = -90f;
    public float rotationSpeed = 5f;

    [Header("Spawn")] public Vector2 spawnPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        agent = GetComponent<FlappyAgent>();
    }

    public void Flap()
    {
        rb.velocity = Vector2.up * jumpForce;
        transform.rotation = Quaternion.Euler(0, 0, maxRotationUp);
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y > maxY)
        {
            Vector3 pos = transform.localPosition;
            pos.y = maxY;
            transform.localPosition = pos;
            if (rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        float t = Mathf.InverseLerp(-10f, 10f, rb.velocity.y);
        float angle = Mathf.Lerp(maxRotationDown, maxRotationUp, t);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.fixedDeltaTime);

        if (animator)
            animator.SetFloat("yForce", rb.velocity.y);
    }
    public void ResetBird()
    {
        transform.localPosition = spawnPos;
        ResetVelocity();
    }
    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
    public float GetGravity()
    {
        return rb.velocity.y;
    }
    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
