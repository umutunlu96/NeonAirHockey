using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hockey : MonoBehaviour
{
    //public AudioClip wallHit;
    //public AudioClip obsHit;
    //public AudioClip finishHit;

    private GameObject shootMaxCircle;
    private LineRenderer lineRenderer;

    private Vector2 direction;
    private Vector2 dragStartPos;
    private Vector2 force;
    private Rigidbody2D rigidBody;
    private GameObject colorChilds;

    public float power = 5;

    private enum BallState
    {
        Moving,
        Idle,
        Finish
    }
    BallState currentState;



    void Awake()
    {
        shootMaxCircle = GameObject.Find("ShootMax");
        lineRenderer = transform.GetComponent<LineRenderer>();
        shootMaxCircle.SetActive(false);
        rigidBody = GetComponent<Rigidbody2D>();

        currentState = BallState.Idle;
        colorChilds = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (rigidBody.velocity.magnitude <= .1f && currentState == BallState.Moving)
        {
            currentState = BallState.Idle;
        }

        if (rigidBody.velocity.magnitude > .1f && currentState == BallState.Idle)
        {
            currentState = BallState.Moving;
        }

    }

    private void OnMouseDown()
    {
        if (currentState == BallState.Idle)
        {
            shootMaxCircle.SetActive(true);

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);

            dragStartPos = this.gameObject.transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (currentState == BallState.Idle)
        {
            Vector2 mausePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Vector2 Mause Input
            direction = dragStartPos - mausePos; //Distance between mause and object

            Vector2 directionMax = Vector2.ClampMagnitude(direction, 1.25f); // Clamp distance

            float distance = Vector2.Distance(dragStartPos, mausePos);
            if (distance > 2f)
                return;
            else
                distance = 2f;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, mausePos);
        }
    }

    private void OnMouseUp()
    {
        if (currentState == BallState.Idle)
        {
            shootMaxCircle.SetActive(false);
            lineRenderer.enabled = false;
            rigidBody.AddForce(direction * power, ForceMode2D.Impulse);
            print(direction.magnitude * power);
        }

    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "WallRed")
        {
            //SoundManager.instance.PlaySoundFX(wallHit, .2f);

            foreach (Transform child in colorChilds.transform)
            {
                child.gameObject.SetActive(false);
            }
            colorChilds.transform.GetChild(0).gameObject.SetActive(true);

        }
        if (target.gameObject.tag == "WallGreen")
        {
            //SoundManager.instance.PlaySoundFX(wallHit, .2f);

            foreach (Transform child in colorChilds.transform)
            {
                child.gameObject.SetActive(false);
            }
            colorChilds.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (target.gameObject.tag == "WallBlue")
        {
            //SoundManager.instance.PlaySoundFX(wallHit, .2f);

            foreach (Transform child in colorChilds.transform)
            {
                child.gameObject.SetActive(false);
            }
            colorChilds.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (target.gameObject.tag == "WallYellow")
        {
            //SoundManager.instance.PlaySoundFX(wallHit, .2f);

            foreach (Transform child in colorChilds.transform)
            {
                child.gameObject.SetActive(false);
            }
            colorChilds.transform.GetChild(3).gameObject.SetActive(true);
        }

        if (target.gameObject.tag == "WallBlack")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2((direction.x > 0 ? 1 : -1), 
                direction.y > 0 ? 1f : -1f), ForceMode2D.Impulse);
        }

        if (target.tag == "Finish")
        {
            //SoundManager.instance.PlaySoundFX(finishHit, .2f);

            rigidBody.velocity = new Vector2(0, 0);
            currentState = BallState.Finish;
            target.tag = "Untagged";
            //Win game UI
        }
    }










}
