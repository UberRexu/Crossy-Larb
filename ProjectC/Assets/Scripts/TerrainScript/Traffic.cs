using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    public TrafficLight RedLight;
    public TrafficLight GreenLight;
    [SerializeField] private GameObject GrabGreen;
    [SerializeField] private GameObject GrabPink;
    [SerializeField] private GameObject Spawnpoint;
    private bool startRedlight = false;
    public AudioSource warning;
    public AudioSource bikePass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startRedlight)
        {
            RedLight.turnLightOnOff();
            GreenLight.turnLightOnOff();
            StartCoroutine(SpawnGrab(15));
            startRedlight = false;
            if (warning != null && bikePass != null)
            {
                warning.Play();
                bikePass.Play();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startRedlight = true;
        }
    }

    IEnumerator SpawnGrab(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Debug.Log("Spawning");
            int WhichCar = Random.Range(1, 3);

            if (WhichCar == 1)
            {
                Instantiate(GrabPink, Spawnpoint.transform);
            }
            else
            {
                Instantiate(GrabGreen, Spawnpoint.transform);
            }
            if (i < amount - 1)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        RedLight.turnLightOnOff();
        GreenLight.turnLightOnOff();
        if (warning != null)
        {
            warning.Stop();
        }
    }
}
