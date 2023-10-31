using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Honk : MonoBehaviour
{
    public AudioSource honk;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            honk.Play();
        }
    }
}
