using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private int playerCoinCount;
    public void UpdatePlayerCoinCount(int newCoinCount)
    {
        playerCoinCount += newCoinCount;
        gameManager.UpdateCoinUI();
    }
    public int GetPlayerCoin()
    {
        return playerCoinCount;
    }
    public void SetPlayerCoin(int newCoinCount)
    {
        playerCoinCount = newCoinCount;
    }

}
