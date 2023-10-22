using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnpauseScreen : MonoBehaviour
{
    public TextMeshProUGUI CountdownText;
    public float timeRemaining = 3f;
    private bool timerIsRunning = false;
    public void Setup()
    {
        gameObject.SetActive(true);
        timeRemaining = 3f;
        timerIsRunning = true;
    }
    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.unscaledDeltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Handle timer completion or game over logic here
                Debug.Log("Timer has run out!");
                GameManager.Instance.UpdateGameState(GameState.Play);
                gameObject.SetActive(false);
            }
        }
    }
    private void UpdateTimerDisplay()
    {
        CountdownText.text = timeRemaining.ToString("F0");
    }
}
