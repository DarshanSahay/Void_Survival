using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : GenericSingleton<ScoreManager>
{
    private int currentScore;
    private int onHitPoints;
    private float powerUpTime;

    [HideInInspector] public bool collectedPowerUp = false;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        GameManager.Instance.onGameStarted += OnGameStarts;
    }
    public void OnGameStarts()
    {
        currentScore = 0;
        onHitPoints = 5;
        powerUpTime = 20f;
        UpdateScore();
    }
    private void Update()
    {
        OnPowerCollected();
    }
    private void OnPowerCollected()
    {
        if (collectedPowerUp)
        {
            onHitPoints = 20;
            powerUpTime -= Time.deltaTime;
            if (powerUpTime <= 0)
            {
                onHitPoints = 5;
                collectedPowerUp = false;
                powerUpTime = 20f;
            }
        }
    }
    public void AddScore()
    {
        currentScore += onHitPoints;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score : " + currentScore.ToString();
    }
    public void OnGameEnd()
    {
        UIManager.Instance.currentScoreText.text = "Current Score : " + currentScore.ToString();

        if(currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
        UIManager.Instance.highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
