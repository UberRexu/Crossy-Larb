using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //State
    public GameState state;
    public static event Action<GameState> OnGameStateChange;

    //GameOver
    public GameOverScreen GameoverScreen;

    //PreStart
    public Prestart Prestart;

    //Win
    public WinScreen WinScreen;

    //Pause
    public PauseScreen PauseScreen;

    //Unpause
    public UnpauseScreen UnpauseScreen;

    //Score
    public float playerScore = 0;
    public TextMeshProUGUI scoreText;
    public GameObject scoreText_GO;

    //Player
    public Player player;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.Prestart);
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        Debug.Log("Game State Changed to: " + newState);
        switch (newState)
        {
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Died:
                HandleDied();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.Unpause:
                HandleUnpause();
                break;
            case GameState.Prestart:
                HandlePreStart();
                break;
            case GameState.Win:
                HandleWin();
                break;
            default:
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    private void HandlePause()
    {
        PauseScreen.Setup();
        player.canMove = false;
    }
    private void HandleUnpause()
    {
        PauseScreen.Setdown();
        UnpauseScreen.Setup();
        player.canMove = false;
    }

    private void HandleDied()
    {
        Debug.Log("Player Died");
        GameoverScreen.Setup(playerScore);
        player.canMove = false;
        scoreText_GO.SetActive(false);
        Time.timeScale = 0;
    }

    private void HandlePlay()
    {
        Time.timeScale = 1; 
        player.canMove = true;
    }
    private void HandlePreStart()
    {
        Prestart.Setup();
    }
    private void HandleWin()
    {
        WinScreen.Setup();
    }
    public void UpdateScore(float points)
    {
        playerScore = points;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore.ToString();
        }
    }
}

public enum GameState
{
    Play,
    Died,
    Pause,
    Unpause,
    Prestart,
    Win
}