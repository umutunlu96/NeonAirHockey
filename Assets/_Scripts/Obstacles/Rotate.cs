using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotate Settings")]


    public bool rotateLeft;
    private bool rotateLeftP;

    public bool rotate360;
    public bool rotateFixAngle;

    [Header("Speed Settings")]
    public float rotateSpeed = 10.0f;
    public bool randomSpeed = false;
    public float minRandomSpeed = 10.0f;
    public float maxRandomSpeed = 30.0f;

    [Header("Fixed Rotate Left Settings")]

    public float leftMaxAngle = 90.0f;
    public float leftMinAngle = 350.0f;

    [Header("Fixed Rotate Right Settings")]

    public float rightMinAngle = 90.0f;
    public float rightMaxAngle = 270.0f;

    private float maxAngle;
    private float minAngle;


    private void Start()
    {
        rotateLeftP = rotateLeft;

        if (rotateLeft)
        {
            minAngle = leftMinAngle;
            maxAngle = leftMaxAngle;
        }
        if (!rotateLeft)
        {
            minAngle = rightMinAngle;
            maxAngle = rightMaxAngle;
        }

        RandomizeSpeed();

    }

    void Update()
    {
        if(rotate360)
            Rotate360(rotateLeftP);

        if (rotateFixAngle)
        {
            RotateFixAngle(rotateLeftP);
        }
    }

    private void Rotate360(bool rotateDirection)
    {
        if (rotateDirection)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);
            RandomizeSpeed();
        }
        else
        {
            transform.Rotate(Vector3.back, Time.deltaTime * rotateSpeed);
            RandomizeSpeed();
        }
    }

    private void RotateFixAngle(bool rotateDirection)
    {
        if (rotateLeft)
        {
            if (rotateDirection)
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);

                if (transform.localEulerAngles.z > maxAngle && transform.localEulerAngles.z < maxAngle + 10)
                {
                    rotateLeftP = !rotateLeftP;
                    RandomizeSpeed();
                }

            }
            else
            {
                transform.Rotate(Vector3.back, Time.deltaTime * rotateSpeed);

                if (transform.localEulerAngles.z > minAngle && transform.localEulerAngles.z > minAngle - 10)
                {
                    rotateLeftP = !rotateLeftP;
                    RandomizeSpeed();
                }

            }
        }

        if (!rotateLeft)
        {
            if (rotateDirection)
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * rotateSpeed);

                if (transform.localEulerAngles.z > minAngle && transform.localEulerAngles.z < minAngle + 10)
                {
                    rotateLeftP = !rotateLeftP;
                    RandomizeSpeed();
                }

            }
            else
            {
                transform.Rotate(Vector3.back, Time.deltaTime * rotateSpeed);

                if (transform.localEulerAngles.z < maxAngle && transform.localEulerAngles.z > maxAngle-10)
                {
                    rotateLeftP = !rotateLeftP;
                    RandomizeSpeed();
                }

            }
        }
    }

    private void RandomizeSpeed()
    {
        if (randomSpeed)
        {
            rotateSpeed = Random.Range(minRandomSpeed, maxRandomSpeed);
        }
    }
}
