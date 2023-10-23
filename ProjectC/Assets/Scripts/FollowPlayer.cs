using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothness;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float forwardSpeed = 20f;

    private bool isMovingForward = false;
    private bool isMovingLeftRight = false;
    private bool isStandingStill = true;

    private bool gameStart = false;

    private void Update()
    {
        if (player.transform.position.x < transform.position.x - 2f)
        {
            GameManager.Instance.UpdateGameState(GameState.Died);
            Debug.Log("Out of bound");
        }
    }
    void LateUpdate()
    {
        if (isMovingForward)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x - 1f, transform.position.y, transform.position.z), ref velocity, smoothness);
        }
        if (isStandingStill && gameStart)
        {
            float forwardPositionX = transform.position.x + forwardSpeed * Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(forwardPositionX, transform.position.y, transform.position.z), ref velocity, smoothness);
        }
        if (isMovingLeftRight)
        {
            float targetPositionX = transform.position.x;
            float targetPositionZ = player.transform.position.z + offset.z;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPositionX, transform.position.y, targetPositionZ), ref velocity, smoothness);
        }
    }

    public void PlayerStartedMovingForward()
    {
        isMovingLeftRight = false;
        isMovingForward = true;
        isStandingStill = false;
    }
    public void PlayerStartedMovingLeftRight()
    {
        isMovingLeftRight = true;
        isMovingForward = false;
        isStandingStill = false;
    }
    public void PlayerStandStill()
    {
        isMovingLeftRight = false;
        isMovingForward = false;
        isStandingStill = true;
    }

    public void StartGame()
    {
        gameStart = true;
    }
}
