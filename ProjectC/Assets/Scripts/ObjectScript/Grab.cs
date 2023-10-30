using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 endPos;
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 0, 22);
    }

    void Update()
    {
        currentPos = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (currentPos.z > endPos.z)
        {
            DestroyCar();
        }
    }
    private void DestroyCar()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateGameState(GameState.Died);
        }
    }
}
