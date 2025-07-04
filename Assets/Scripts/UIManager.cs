using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private List<Menu> menus;
    private GameManager gameManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;

    [SerializeField] private TMP_Text scoreTextEnd;
    [SerializeField] private TMP_Text highscoreTextEnd;

    [SerializeField] private TMP_Text readyText;

    [Header("Block")]
    [SerializeField] private GameObject block;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnEnable()
    {
        gameManager.OnScoreChanged.AddListener(ChangeScoreText);
        gameManager.OnHighScoreChanged.AddListener(ChangeHighScore);
    }
    private void OnDisable()
    {
        gameManager.OnScoreChanged.RemoveListener(ChangeScoreText);
        gameManager.OnHighScoreChanged.RemoveListener(ChangeHighScore);
    }
    public void ActiveMenu(string name)
    {
        menus.ForEach(m =>
        {
            if (m.nameMenu == name)
            {
                m.Open();
            }
            else m.Close();
        });
        block.SetActive(true);
    }
    public void CloseAllMenu()
    {
        menus.ForEach(m =>
        {
            if(m.isactive)
            {
                m.Close();
            }
        });
        block.SetActive(false);
    }
    public void ChangeScoreText(int score)
    {
        scoreText.text = score.ToString();
        scoreTextEnd.text = score.ToString();
    }
    public void ChangeHighScore(int score)
    {
        highscoreText.text = $"HIGH SCORE: "+ score.ToString();
        highscoreTextEnd.text = score.ToString();

    }
    public void ActiveReady()
    {
        readyText.gameObject.SetActive(true);
    }
}
