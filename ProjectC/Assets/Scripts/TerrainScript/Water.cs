using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public AudioSource fallInWater;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (fallInWater != null)
            {
                fallInWater.Play();
            }
            GameManager.Instance.UpdateGameState(GameState.Died);
        }
    }
}
