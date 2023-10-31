using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isMenu : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Awake || State = menu");
        GameManager.Instance.UpdateGameState(GameState.Menu);
    }
}
