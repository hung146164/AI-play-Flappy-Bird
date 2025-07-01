using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private ObstacleManager obstacleManager;

    private ObjectPooling objectPooling;

    public Vector2 obstacleSpawnPos;

    public float spawnTime = 0.5f;

    public bool cooldown = false;
    public bool isPlaying = false;
    public bool isOver = false;
    public bool isPause = false;
    private UIManager uIManager;

    private int score;
    private int highscore;

    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<int> OnHighScoreChanged;

    private Player player;


    public int Score { 
        get => score; 
        set
        {
            score = value;
            OnScoreChanged?.Invoke(value);
        }
    }
    public int Highscore
    {
        get => highscore;
        set
        {
            highscore = value;
            OnHighScoreChanged?.Invoke(value);
        }
    }

    private void Awake()
    {
        obstacleManager = FindObjectOfType<ObstacleManager>();
        uIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
        objectPooling = FindObjectOfType<ObjectPooling>();
    }
    private void Start()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isOver && !isPause)
        {
            SoundManager.instance.Wing();
            Time.timeScale = 1f;
            isPlaying = true;
        }
    }
   
    private void FixedUpdate()
    {
        if (isOver|| isPause || !isPlaying) return;

        if(!cooldown)
        {
            StartCoroutine(CreatePipe(spawnTime));
        }
    }


    private IEnumerator CreatePipe(float spawnTime)
    {
        cooldown = true;
        obstacleManager.CreatePipe(obstacleSpawnPos);
        yield return new WaitForSeconds(spawnTime);
        cooldown = false;
    }
    public void GameOver()
    {
        SoundManager.instance.HitSfx();
        isOver = true;
        Time.timeScale = 0f;
        ChangeHighScore();
        uIManager.ActiveMenu("gameover");
        
    }

    public void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
        player.ResetVelocity();
    }
    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0f; 
    }


    public void AddScore(int score)
    {
        SoundManager.instance.PointSfx();

        Score++;
    }
    public void ChangeHighScore()
    {
        if(Score > Highscore)
        {
            Highscore = Score;
        }
    }
    public void ResetGame()
    {
        Time.timeScale = 0f;
        player.ResetBird();
        player.ResetVelocity();
        ResetScore();
        ResetUI();
        ResetObstacles();
        SetReadyUI();
        isOver = false;
    }

    private void ResetScore()
    {
        Score = 0;
    }
    private void ResetObstacles()
    {
        objectPooling.ResetAllObjects();
    }
    private void ResetUI()
    {
        uIManager.ResetUI();
    }
    public void SetReadyUI()
    {
        uIManager.ActiveReady();
    }

}
