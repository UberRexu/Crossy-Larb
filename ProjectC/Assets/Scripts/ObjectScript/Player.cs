using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public bool isMoving;
    public bool canMove = true;

    [SerializeField] private TerrainGenerator terrainGenerator;
    public float score;

    [SerializeField] GameManager gameManager;
    [SerializeField] CoinManager coinManager;

    [SerializeField] private FollowPlayer Camera;

    private bool isPause = false;
    public bool isDead = false;

    public int coinCount = 0;

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
                    Camera.PlayerStartedMovingForward();
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) && !isMoving)
            {
                inputDirection = new Vector3(0, 0, 1);
                Vector3 newPosition = transform.position + inputDirection;
                if (CanMoveToPosition(newPosition, Vector3.forward))
                {
                    MoveCharacter(inputDirection);
                    Camera.PlayerStartedMovingLeftRight();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
            {
                inputDirection = new Vector3(0, 0, -1);
                Vector3 newPosition = transform.position + inputDirection;
                if (CanMoveToPosition(newPosition, Vector3.back))
                {
                    MoveCharacter(inputDirection);
                    Camera.PlayerStartedMovingLeftRight();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause();
        }
        gameManager.UpdateScore(UpdateScore());
    }

    private bool CanMoveToPosition(Vector3 newPosition, Vector3 whichway)
    {
        // Set the raycast origin at the player's feet.
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;
        // Cast raycasts in all directions to check for obstacles.
        float raycastDistance = 1.0f;
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, whichway, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                return false;
            }
        }
        return true;
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("Move");
        isMoving = true;
        StartCoroutine(MoveWithAnimation(difference));

        if (terrainGenerator != null)
        {
            terrainGenerator.SpawnTerrains(false, false, transform.position);
        }
    }

    IEnumerator MoveWithAnimation(Vector3 difference)
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = (transform.position + difference);
    }
    public void FinishMove()
    {
        isMoving = false;
        Camera.PlayerStandStill();
    }
    
    public float UpdateScore()
    {
        score = transform.position.x;
        score = Mathf.CeilToInt(score);
        return (int)score;
    }

    public void HandlePause()
    {
        if (!isPause)
        {
            PauseGame();
            isPause = true;
        }
        else
        {
            UnPauseGame();
            isPause = false;
        }
    }
    public void PauseGame()
    {
        GameManager.Instance.UpdateGameState(GameState.Pause);
    }
    public void UnPauseGame()
    {
        GameManager.Instance.UpdateGameState(GameState.Unpause);
    }
    public void Dead()
    {
        animator.SetTrigger("isDead");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
            coinManager.UpdatePlayerCoinCount(1);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Debug.Log("Collected Coin");
        coinCount++;
        Destroy(coin);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}