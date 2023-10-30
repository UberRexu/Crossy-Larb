using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    Light pointLight;
    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    public void turnLightOnOff()
    {
        pointLight.enabled = !pointLight.enabled;
    }
}
