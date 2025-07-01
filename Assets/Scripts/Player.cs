using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private GameManager gamemanager;

    [SerializeField] private Vector2 spawnPos;

    [SerializeField] private float groundY;

    [Range(1,10)] public float jumpForce;

    [SerializeField] private float maxY;


    [SerializeField] private float maxRotationUp = 30f;
    [SerializeField] private float maxRotationDown = -90f;
    [SerializeField] private float rotationSpeed = 5f;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gamemanager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            rb.velocity = new Vector2(0, jumpForce);
            transform.rotation = Quaternion.Euler(0, 0, maxRotationUp); // bay lên xoay lên
        }

    }
    private void FixedUpdate()
    {
        if(transform.position.y< groundY)
        {
            gamemanager.GameOver();
        }
        if (transform.position.y > maxY)
        {
            Vector3 pos = transform.position;
            pos.y = maxY;
            transform.position = pos;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        float angle = Mathf.Lerp(maxRotationDown, maxRotationUp, (rb.velocity.y + 10f) / 20f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.fixedDeltaTime);

        animator.SetFloat("yForce",rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            gamemanager.GameOver();
        }
        else if(collision.CompareTag("TriggerPoint"))
        {
            gamemanager.AddScore(1);
        }
    }
    
    public void ResetBird()
    {
        transform.position = spawnPos;
        transform.rotation = Quaternion.identity;
    }
    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

}
