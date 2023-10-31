using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transiton;
    public float transitionTime = 2f;

    public void LoadNextLevel(string LevelName)
    {
        StartCoroutine(LoadLevel(LevelName));
    }

    IEnumerator LoadLevel(string LevelName)
    {
        transiton.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}


