using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject playerJoystick;
    [SerializeField] private GameObject playerScoreBox;
    [SerializeField] private GameObject playerHealthBox;
    [SerializeField] private GameObject playerShootBtnBox;
    [SerializeField] private GameObject pauseBtnBox;
    [SerializeField] private GameObject enemySpawner;

    [SerializeField] private Button startGameBtn;
    [SerializeField] private Button quitGameBtn;
    [SerializeField] private Button menuBtn1;
    [SerializeField] private Button menuBtn2;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button pauseBtn;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public Slider playerHealthBar;

    private void Start()
    {
        startGameBtn.onClick.AddListener(SetGameRunningState);
        quitGameBtn.onClick.AddListener(QuitGame);
        menuBtn1.onClick.AddListener(SetGameStartedState);
        menuBtn2.onClick.AddListener(SetGameStartedState);
        continueBtn.onClick.AddListener(SetGameRunningState);
        retryBtn.onClick.AddListener(ResetGameState);
        pauseBtn.onClick.AddListener(SetGamePauseState);
    }
    public void OnGameStarted()
    {
        playerJoystick.gameObject.SetActive(false);
        playerScoreBox.gameObject.SetActive(false);
        playerHealthBox.gameObject.SetActive(false);
        playerShootBtnBox.gameObject.SetActive(false);
        pauseBtnBox.gameObject.SetActive(false);
        enemySpawner.gameObject.SetActive(false);
    }
    public void OnGameRunning()
    {
        playerJoystick.gameObject.SetActive(true);
        playerScoreBox.gameObject.SetActive(true);
        playerHealthBox.gameObject.SetActive(true);
        playerShootBtnBox.gameObject.SetActive(true);
        pauseBtnBox.gameObject.SetActive(true);
        enemySpawner.gameObject.SetActive(true);
    }
    void SetGameStartedState()
    {
        ObjectPool.Instance.DeActivateAll();
        GameManager.Instance.SetGameState(GameStates.GameStarted);
    }
    void SetGameRunningState()
    {
        GameManager.Instance.SetGameState(GameStates.GameRunning);
    }
    void SetGamePauseState()
    {
        if (GameManager.Instance.state == GameStates.GameRunning)
        {
            GameManager.Instance.SetGameState(GameStates.GamePaused);
        }
    }
    void ResetGameState()
    {
        ObjectPool.Instance.DeActivateAll();
        GameManager.Instance.SetGameState(GameStates.GameReseted);
    }
    public void OpenMainMenuScreen()
    {
        mainMenuPanel.gameObject.SetActive(true);
    }
    public void CloseMainMenuScreen()
    {
        mainMenuPanel.gameObject.SetActive(false);
    }
    public void OpenGameOverScreen()
    {
        gameOverPanel.gameObject.SetActive(true);
    }
    public void CloseGameOverScreen()
    {
        gameOverPanel.gameObject.SetActive(false);
    }
    public void OpenPauseScreen()
    {
        pausePanel.gameObject.SetActive(true);
    }
    public void ClosePauseScreen()
    {
        pausePanel.gameObject.SetActive(false);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
