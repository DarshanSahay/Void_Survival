using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameStates                      // enum to keep track of game state
{
    GameStarted,
    GameRunning,
    GamePaused,
    GameReseted,
    GameEnded
}
public class GameManager : GenericSingleton<GameManager>
{
    public GameStates state;
    private UIManager ui;
    private ScoreManager _sManager;
    public ShipHealth health;
    
    public event Action onGameStarted;                              //events related to game states
    private event Action onGameRunning;
    private event Action onGamePaused;
    private event Action onGameReseted;
    private event Action onGameEnded;
    
    private void Awake()
    {
        ui = UIManager.Instance;
        _sManager = ScoreManager.Instance;
        InitialiseEvents();
    }
    private void Start()
    {
        SetGameState(GameStates.GameStarted);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetGameState(GameStates.GamePaused);
        }
    }
    private void InitialiseEvents()                                             //Subscribing all events according to the state
    {
        onGameStarted += ui.OpenMainMenuScreen;
        onGameStarted += ui.OnGameStarted;
        onGameStarted += ui.ClosePauseScreen;
        onGameStarted += ui.CloseGameOverScreen;

        onGameRunning += ui.CloseMainMenuScreen;
        onGameRunning += health.SetHealth;
        onGameRunning += ui.OnGameRunning;
        onGameRunning += ui.ClosePauseScreen;

        onGamePaused += ui.OpenPauseScreen;

        onGameReseted += health.SetHealth;
        onGameReseted += ui.CloseGameOverScreen;
        onGameReseted += _sManager.OnGameStarts;

        onGameEnded += _sManager.OnGameEnd;
        onGameEnded += ui.OpenGameOverScreen;
        onGameEnded += ui.ClosePauseScreen;
    }
    public void SetGameState(GameStates State)                         //function to set state of game
    {
        state = State;
        UpdateGameState();
    }
    private void UpdateGameState()                                      //function to update the game state and invoke the events related to it
    {
        switch (state)
        {
            case GameStates.GameStarted:
                onGameStarted?.Invoke();
                break;

            case GameStates.GameRunning:
                Time.timeScale = 1;
                onGameRunning?.Invoke();
                break;

            case GameStates.GamePaused:
                Time.timeScale= 0;
                onGamePaused?.Invoke();
                break;

            case GameStates.GameReseted:
                Time.timeScale = 1;
                onGameReseted?.Invoke();
                break;

            case GameStates.GameEnded:
                Time.timeScale = 0;
                onGameEnded?.Invoke();
                break;
        }
    }
}
