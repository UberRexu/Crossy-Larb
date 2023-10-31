using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnlockManager : MonoBehaviour
{
    private Dictionary<int, bool> unlockedSkins = new Dictionary<int, bool>();

    public void Start()
    {
        unlockedSkins[0] = true;
        PlayerPrefs.SetInt("Item_" + 0, 1);
        PlayerPrefs.Save();
    }
    public void UnlockItem(int itemId)
    {
        unlockedSkins[itemId] = true;
        // Store the unlocking status in PlayerPrefs.
        PlayerPrefs.SetInt("Item_" + itemId, 1);
        PlayerPrefs.Save();
    }

    public bool IsItemUnlocked(int itemId)
    {
        if (unlockedSkins.ContainsKey(itemId))
        {
            return unlockedSkins[itemId];
        }
        // Check the unlocking status in PlayerPrefs.
        return PlayerPrefs.GetInt("Item_" + itemId, 0) == 1;
    }
}
