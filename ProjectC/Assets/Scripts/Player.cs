using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private bool isMoving;

    [SerializeField] private TerrainGenerator terrainGenerator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Move Forward
        if (Input.GetKeyDown(KeyCode.W) && !isMoving)
        {
            animator.SetTrigger("Move");
            isMoving = true;
            //Getting Back onto a grid and movefoward
            float zDiff = 0;
            if (transform.position.z % 1 != 0)
            {
                zDiff = Mathf.RoundToInt(transform.position.z) - transform.position.z;
            }
            MoveCharacter(new Vector3(1, 0, zDiff));
        }

        //Move Left
        else if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {
            MoveCharacter(new Vector3(0, 0, 1));
        }
        //Move Right
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            MoveCharacter(new Vector3 (0, 0, -1));
        }
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("Move");
        isMoving = true;
        transform.position = (transform.position + difference);
        terrainGenerator.SpawnTerrains(false, transform.position);
    }

    public void FinishMove()
    {
        isMoving = false;
    }
}
