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

    //Score
    public float playerScore = 0;
    public TextMeshProUGUI scoreText;

    //Player
    public Player player;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.Play);
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
            default:
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }

    private void HandlePause()
    {
        
    }

    private void HandleDied()
    {
        Debug.Log("Player Died");
        GameoverScreen.Setup(playerScore);
        Debug.Log("Before setting player.canMove: " + player.canMove);
        player.canMove = false;
        Debug.Log("After setting player.canMove: " + player.canMove);
    }

    private void HandlePlay()
    {
        
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
    Pause
}