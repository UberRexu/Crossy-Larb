using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Material defaultShirtMaterial;
    public Renderer characterRenderer;
    private Material currentShirtMaterial;
    public Material[] shirtMaterials;
    [SerializeField] ItemUnlockManager itemUnlockManager;
    [SerializeField] ShopManager shopManager;
    [SerializeField] private int SelectedMat;

    // Start is called before the first frame update
    void Start()
    {
        SelectedMat = 1;
    }

    private void Awake()
    {
        int SelectedMat = PlayerPrefs.GetInt("ShirtMaterialIndex", 0);
        ChangeShirtMaterial(SelectedMat);
        ApplyMaterials();
    }

    public void ChangeShirtMaterial(int matSelected)
    {
        if (itemUnlockManager.IsItemUnlocked(matSelected))
        {
            if (matSelected >= 0 && matSelected < shirtMaterials.Length)
            {
                int newMat = matSelected;
                currentShirtMaterial = shirtMaterials[matSelected];
                PlayerPrefs.SetInt("ShirtMaterialIndex", newMat);
                PlayerPrefs.Save();
                ApplyMaterials();
                Debug.Log("ApplyMat: Method");
            }
        }
        else
        {
            shopManager.BuySkin(matSelected);
        }
    }

    private void ApplyMaterials()
    {
        Material[] originalMaterials = characterRenderer.materials;
        Material[] newMaterials = new Material[originalMaterials.Length];

        for (int i = 0; i < originalMaterials.Length; i++)
        {
            if (i == 0)
            {
                if (currentShirtMaterial != null)
                {
                newMaterials[i] = new Material(currentShirtMaterial);
                }
                else
                {
                newMaterials[i] = originalMaterials[i];
                }
            }
            else
            {
                newMaterials[i] = originalMaterials[i];
            }
        }

        // Apply the modified materials to the character's renderer.
        characterRenderer.materials = newMaterials;
    }
}
