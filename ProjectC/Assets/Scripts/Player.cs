using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private bool isMoving;
    public bool canMove = true;

    [SerializeField] private TerrainGenerator terrainGenerator;
    public float score;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            // Store the input direction.
            Vector3 inputDirection = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W) && !isMoving)
            {
                inputDirection = new Vector3(1, 0, 0);
                Vector3 newPosition = transform.position + inputDirection;
                if (CanMoveToPosition(newPosition, Vector3.right))
                {
                    MoveCharacter(inputDirection);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) && !isMoving)
            {
                inputDirection = new Vector3(0, 0, 1);
                Vector3 newPosition = transform.position + inputDirection;
                if (CanMoveToPosition(newPosition, Vector3.forward))
                {
                    MoveCharacter(inputDirection);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
            {
                inputDirection = new Vector3(0, 0, -1);
                Vector3 newPosition = transform.position + inputDirection;
                if (CanMoveToPosition(newPosition, Vector3.back))
                {
                    MoveCharacter(inputDirection);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) && !isMoving)
            {
                inputDirection = new Vector3(-1, 0, 0);
                Vector3 newPosition = transform.position + inputDirection;
                if (CanMoveToPosition(newPosition, Vector3.left))
                {
                    MoveCharacter(inputDirection);
                }
            }
        }

        gameManager.UpdateScore(UpdateScore());
    }

    private bool CanMoveToPosition(Vector3 newPosition, Vector3 whichway)
    {
        // Set the raycast origin at the player's feet.
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;
        // Cast raycasts in all directions to check for obstacles.
        float raycastDistance = 1.0f;
        Debug.Log("Transform" + transform.position);
        Debug.Log("Raycast " + whichway);
        Debug.Log("newPos " + newPosition);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, whichway, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.Log("Raycast hit: " + hit.collider.name);
                return false;
            }
        }
        return true;
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("Move");
        isMoving = true;
        transform.position = (transform.position + difference);

        if (terrainGenerator != null)
        {
            terrainGenerator.SpawnTerrains(false, false, transform.position);
        }
    }

    public void FinishMove()
    {
        isMoving = false;
    }

    public float UpdateScore()
    {
        score = transform.position.x;
        return score;
    }
}