using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PipeManager pipeManager;
    private UIManager uiManager;

    [SerializeField] private Player player;
    [SerializeField] private FlappyAgent flappyAgent;

    [Header("Gameplay")]
    public float spawnTime = 0.5f;

    private float timer;

    public bool isOver { get; private set; } = false;
    public bool isPause { get; private set; } = true;

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
    }

    private void FixedUpdate()
    {
        if (isOver) return;
        if (isPause) return;
        timer += Time.fixedDeltaTime;

        if (timer >= spawnTime)
        {
            pipeManager.CreatePipe();
            timer = 0f;
        }
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
        isOver = false;
        pipeManager.ResetPipes();
        player.ResetPlayer();

        uiManager?.CloseAllMenu();
        //uiManager?.ActiveReady();

        ResetScore();
        Resume();
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void ChangeHighScore()
    {
        if (Score > Highscore)
            Highscore = Score;
    }

    public void AiPlay()
    {
        flappyAgent.gameObject.SetActive(true);
        uiManager.CloseAllMenu();
        isPause = false;
    }

    public void PlayerPlay()
    {
        Debug.Log(player == null ? "yes" : "NO");
        player.gameObject.SetActive(true);
        uiManager.CloseAllMenu();
        isPause = false;
    }

    public void QuitGame()
    {
        Application.Quit();  
    }
}