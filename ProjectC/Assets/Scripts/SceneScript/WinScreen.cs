using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public void Setup(float score)
    {
        gameObject.SetActive(true);
        pointText.text = score.ToString() + " POINTS";
    }
}
