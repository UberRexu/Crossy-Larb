using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private int playerCoinCount;
    public void UpdatePlayerCoinCount(int newCoinCount)
    {
        playerCoinCount = newCoinCount;
    }
    public int GetPlayerCoinCount()
    {
        return playerCoinCount;
    }
}
