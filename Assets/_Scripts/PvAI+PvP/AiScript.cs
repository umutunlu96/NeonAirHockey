using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiScript : MonoBehaviour
{
    public float maxMovementSpeed;
    private Rigidbody2D rigidBody;
    private Vector2 startingPosition;

    public Rigidbody2D ball;
    public Transform playerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform ballBoundaryHolder;
    private Boundary ballBoundary;

    private Vector2 targetPosition;

    private bool isFirstTimeInOpponentsHalf = true;
    private bool canAiMove = false;
    private float offsetXFromTarget;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        startingPosition = rigidBody.position;

        StartCoroutine(delayAI());

        playerBoundary = new Boundary(playerBoundaryHolder.GetChild(0).position.y,
                              playerBoundaryHolder.GetChild(1).position.y,
                              playerBoundaryHolder.GetChild(2).position.x,
                              playerBoundaryHolder.GetChild(3).position.x);

        ballBoundary = new Boundary(ballBoundaryHolder.GetChild(0).position.y,
                      ballBoundaryHolder.GetChild(1).position.y,
                      ballBoundaryHolder.GetChild(2).position.x,
                      ballBoundaryHolder.GetChild(3).position.x);

        //Difficulties
        switch (AiSettings.Difficulty)
        {
            case AiSettings.Difficulties.Easy:
                maxMovementSpeed = 10;
                break;
            case AiSettings.Difficulties.Medium:
                maxMovementSpeed = 15;
                break;
            case AiSettings.Difficulties.Hard:
                maxMovementSpeed = 20;
                break;
        }
    }

    void FixedUpdate()
    {
        if (!BallScript.isGoal && canAiMove)
        {
            float movementSpeed;

            if (ball.position.y < ballBoundary.Down)
            {
                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1, 1);
                }
                movementSpeed = maxMovementSpeed * Random.Range(0.1f, 0.3f);
                targetPosition = new Vector2(Mathf.Clamp(ball.position.x + offsetXFromTarget, playerBoundary.Left, playerBoundary.Right), startingPosition.y);
            }
            else
            {
                isFirstTimeInOpponentsHalf = true;
                movementSpeed = Random.Range(maxMovementSpeed * 0.4f, maxMovementSpeed);
                targetPosition = new Vector2(Mathf.Clamp(ball.position.x, playerBoundary.Left, playerBoundary.Right),
                                             Mathf.Clamp(ball.position.y, playerBoundary.Down, playerBoundary.Up));
            }

            rigidBody.MovePosition(Vector2.MoveTowards(rigidBody.position, targetPosition,
                                    movementSpeed * Time.fixedDeltaTime));
        }
    }

    IEnumerator delayAI()
    {
        yield return new WaitForSeconds(1);
        canAiMove = true;
    }
}