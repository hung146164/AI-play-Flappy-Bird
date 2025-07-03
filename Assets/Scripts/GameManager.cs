using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PipeManager pipeManager;
    private Player player;
    private UIManager uiManager;

    [Header("Gameplay")]
    public float spawnTime = 0.5f;
    private bool iscooldown;

    public bool isOver { get; private set; } = false;
    public bool isPause { get; private set; } = false;

    private int score;
    private int highscore;
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<int> OnHighScoreChanged;

    public int Score
    {
        get => score;
        private set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }
    public int Highscore
    {
        get => highscore;
        private set
        {
            highscore = value;
            OnHighScoreChanged?.Invoke(highscore);
        }
    }

    private void Awake()
    {
        pipeManager = FindObjectOfType<PipeManager>();
        uiManager = FindObjectOfType<UIManager>();
        player=FindObjectOfType<Player>();  
    }
    private void FixedUpdate()
    {
        if (isOver) return;
        if(isPause) return;
        if (iscooldown) return;
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        iscooldown = true;
        pipeManager.CreatePipe();
        yield return new WaitForSeconds(spawnTime);
        iscooldown = false;
    }

    public void AddScore(int amt)
    {
        Score += amt;
        SoundManager.instance?.PointSfx();
    }

    public void GameOver()
    {
        isOver = true;
        PauseGame();
        ChangeHighScore();
        SoundManager.instance?.HitSfx();
        uiManager?.ActiveMenu("gameover");
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPause = false;
        Time.timeScale = 1f;
    }

    public void ResetGame()
    {
        Time.timeScale = 0f;
        isOver = false;
        pipeManager.ResetPipes();
        player.ResetPlayer();

        uiManager?.ResetUI();
        //uiManager?.ActiveReady();

        ResetScore();
        Resume();
    }
    public void ResetScore()
    {
        Score = 0;
    }
    private void ChangeHighScore()
    {
        if (Score > Highscore)
            Highscore = Score;
    }
}
