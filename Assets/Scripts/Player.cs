using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BirdController bird;
    private GameManager gameManager;

    public float groundY = 2.85f;


    private void Start()
    {
        bird = GetComponent<BirdController>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bird.Flap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
        if (collision.CompareTag("TriggerPoint"))
        {
            gameManager.AddScore(1);
        }
        if(collision.CompareTag("Ground"))
        {
            gameManager.GameOver();
        }
    }
    public void ResetPlayer()
    {
        bird.ResetBird();
    }
}
