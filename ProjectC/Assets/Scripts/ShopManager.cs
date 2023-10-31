using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private int playerCoin;
    private int price = 100;
    [SerializeField] CoinManager coinManager;
    [SerializeField] ItemUnlockManager itemUnlockManager;
    public TextMeshProUGUI item1Text;
    public TextMeshProUGUI item2Text;
    public TextMeshProUGUI item3Text;
    public TextMeshProUGUI item4Text;
    public TextMeshProUGUI item5Text;
    public TextMeshProUGUI item6Text;

    public void BuySkin(int ItemID)
    {
        playerCoin = coinManager.GetPlayerCoin();
        if (playerCoin >= price)
        {
            BuySuccessful(ItemID);
        }
        else
        {

        }
    }
    public void Update()
    {
        if (itemUnlockManager.IsItemUnlocked(0))
        {
            if (item1Text != null)
            {
                item1Text.text = "Equip";
            }   
        }
        if (itemUnlockManager.IsItemUnlocked(1))
        {
            if (item2Text != null)
            {
                item2Text.text = "Equip";
            }
        }
        if (itemUnlockManager.IsItemUnlocked(2))
        {
            if (item3Text != null)
            {
                item3Text.text = "Equip";
            }
        }
        if (itemUnlockManager.IsItemUnlocked(3))
        {
            if (item4Text != null)
            {
                item4Text.text = "Equip";
            }
        }
        if (itemUnlockManager.IsItemUnlocked(4))
        {
            if (item5Text != null)
            {
                item5Text.text = "Equip";
            }
        }
        if (itemUnlockManager.IsItemUnlocked(5))
        {
            if (item6Text != null)
            {
                item6Text.text = "Equip";
            }
        }
    }

    public void BuySuccessful(int ItemID)
    {
        playerCoin -= price;
        coinManager.SetPlayerCoin(playerCoin);
        Debug.Log("Bought");
        itemUnlockManager.UnlockItem(ItemID);
    }

    
}
