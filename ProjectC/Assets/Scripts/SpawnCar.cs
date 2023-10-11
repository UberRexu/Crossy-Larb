using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public float speed = 5.0f;
    public float despawnX = 10f;
    public float spawnInterval = 2.0f;
    private float timer = 0.0f;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject car;

    void Start()
    {
        
    }

    
    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a car
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0.0f;

            // Spawn a car at the spawn point
            Instantiate(car, spawnpoint.transform);
        }

    }
}
