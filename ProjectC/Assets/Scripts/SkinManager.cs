using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Material defaultShirtMaterial;
    public Renderer characterRenderer;
    private Material currentShirtMaterial;

    // Start is called before the first frame update
    void Start()
    {
        LoadShirtMaterial(currentShirtMaterial);
        ApplyMaterials();
    }

    public void ChangeShirtMaterial(Material newShirtMaterial)
    {
        int newMaterialIndex = 0;
        currentShirtMaterial = newShirtMaterial;
        PlayerPrefs.SetInt("ShirtMaterialIndex", newMaterialIndex);
        PlayerPrefs.Save();
        ApplyMaterials();
        Debug.Log("ApplyMat: Method");
    }

    private void ApplyMaterials()
    {
        Material[] originalMaterials = characterRenderer.materials;
        Material[] newMaterials = new Material[originalMaterials.Length];

        for (int i = 0; i < originalMaterials.Length; i++)
        {
            if (i == 0)
            {
                newMaterials[i] = new Material(currentShirtMaterial);
            }
            else
            {
                newMaterials[i] = originalMaterials[i];
            }
        }

        // Apply the modified materials to the character's renderer.
        characterRenderer.materials = newMaterials;
    }
    private void LoadShirtMaterial(Material currentMat)
    {
        int savedMaterialIndex = PlayerPrefs.GetInt("ShirtMaterialIndex", 0);
        // Load the corresponding shirt material based on the saved index.
        currentShirtMaterial = currentMat;
    }
}
