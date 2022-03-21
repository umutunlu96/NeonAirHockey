using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    [Header("Translate Settings")]
    public float moveAmount;
    public bool goBase;

    [Header("Speed Settings")]
    public float moveSpeed;
    public bool randomSpeed;
    public float minMoveSpeed;
    public float maxMoveSpeed;

    [Header("Direction Settings")]
    public bool moveForward;
    public bool moveBackward;
    public bool moveLeft;
    public bool moveRight;

    private bool moveForwardp;
    private bool moveBackwardp;
    private bool moveLeftp;
    private bool moveRightp;

    private float xStartPosition;
    private float yStartPosition;

    private float xPosition;
    private float yPosition;

    private void Start()
    {
        xStartPosition = transform.position.x;
        yStartPosition = transform.position.y;

        moveForwardp = moveForward;
        moveBackwardp = moveBackward;
        moveLeftp = moveLeft;
        moveRightp = moveRight;

        RandomizeSpeed();
    }

    private void Update()
    {
        //Update Currrent Position
        xPosition = transform.position.x;
        yPosition = transform.position.y;

        ControlDirection();
    }

    private void RandomizeSpeed()
    {
        if (randomSpeed)
        {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        }
    }

    private void ControlDirection()
    {
        if (goBase)
        {
            if (moveForward)
            {
                if (moveForwardp)
                {
                    MoveForward();

                    if (yPosition > yStartPosition + moveAmount)
                    {
                        moveForwardp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveBackward();

                    if (yPosition <= yStartPosition)
                    {
                        moveForwardp = true;
                        RandomizeSpeed();
                    }
                }
            }

            else if (moveBackward)
            {
                if (moveBackwardp)
                {
                    MoveBackward();

                    if (yPosition < yStartPosition - moveAmount)
                    {
                        moveBackwardp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveForward();

                    if (yPosition >= yStartPosition)
                    {
                        moveBackwardp = true;
                        RandomizeSpeed();
                    }
                }
            }

            else if (moveLeft)
            {
                if (moveLeftp)
                {
                    MoveLeft();

                    if (xPosition < xStartPosition - moveAmount)
                    {
                        moveLeftp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveRight();

                    if (xPosition >= xStartPosition)
                    {
                        moveLeftp = true;
                        RandomizeSpeed();
                    }
                }
            }

            else if (moveRight)
            {
                if (moveRightp)
                {
                    MoveRight();
                    
                    if (xPosition > xStartPosition + moveAmount)
                    {
                        moveRightp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveLeft();

                    if (xPosition <= xStartPosition)
                    {
                        moveRightp = true;
                        RandomizeSpeed();
                    }
                }
            }
        }


        else
        {
            if (moveForward)
            {
                if (moveForwardp)
                {
                    MoveForward();

                    if (yPosition > yStartPosition + moveAmount)
                    {
                        moveForwardp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveBackward();

                    if (yPosition <= yStartPosition - moveAmount)
                    {
                        moveForwardp = true;
                        RandomizeSpeed();
                    }
                }
            }

            else if (moveBackward)
            {
                if (moveBackwardp)
                {
                    MoveBackward();

                    if (yPosition < yStartPosition - moveAmount)
                    {
                        moveBackwardp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveForward();

                    if (yPosition >= yStartPosition + moveAmount)
                    {
                        moveBackwardp = true;
                        RandomizeSpeed();
                    }
                }
            }

            else if (moveLeft)
            {
                if (moveLeftp)
                {
                    MoveLeft();

                    if (xPosition < xStartPosition - moveAmount)
                    {
                        moveLeftp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveRight();

                    if (xPosition >= xStartPosition + moveAmount)
                    {
                        moveLeftp = true;
                        RandomizeSpeed();
                    }
                }
            }

            else if (moveRight)
            {
                if (moveRightp)
                {
                    MoveRight();

                    if (xPosition > xStartPosition + moveAmount)
                    {
                        moveRightp = false;
                        RandomizeSpeed();
                    }
                }

                else
                {
                    MoveLeft();

                    if (xPosition <= xStartPosition - moveAmount)
                    {
                        moveRightp = true;
                        RandomizeSpeed();
                    }
                }
            }
        }
    }


    private void MoveForward()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }

    private void MoveBackward()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

}
