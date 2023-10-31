using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 endPos;
    private bool movingForward = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 0, 8);
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        if (movingForward)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (currentPos.z >= endPos.z)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (currentPos.z <= startPos.z)
            {
                movingForward = true;
            }
        }
    }
}
