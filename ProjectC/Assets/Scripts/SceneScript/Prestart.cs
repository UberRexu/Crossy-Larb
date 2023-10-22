using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Prestart : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(keyCode))
            {
                // Do something when any key is pressed.
                Debug.Log("Key " + keyCode + " is pressed.");
                GameManager.Instance.UpdateGameState(GameState.Play);
                gameObject.SetActive(false);
            }
        }
    }
}
