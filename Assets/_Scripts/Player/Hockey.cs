using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hockey : MonoBehaviour
{
    //public AudioClip wallHit;
    //public AudioClip obsHit;
    //public AudioClip finishHit;

    public int tryAmount;

    public float power = 5;
    public float lineRendererMaxRange = 3;

    public bool setMaxSpeed = false;
    public float maxSpeed = 5;

    private GameManager gamaManager;


    private GameObject shootMaxCircle;
    private LineRenderer lineRenderer;
    private Vector3 startPos, endPos;

    private Vector2 direction;
    private Vector2 dragStartPos;
    private Vector2 force;
    private Rigidbody2D rigidBody;
    private GameObject colorChilds;



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

        gamaManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {
        if (rigidBody.velocity.magnitude <= .3f && currentState == BallState.Moving)
        {
            currentState = BallState.Idle;
        }

        else if (rigidBody.velocity.magnitude > .1f && currentState == BallState.Idle)
        {
            currentState = BallState.Moving;
        }

        if (rigidBody.velocity.magnitude < .15f)
            rigidBody.velocity *= rigidBody.velocity * .95f;
        print(rigidBody.velocity.magnitude);
    }

    private void OnMouseDown()
    {
        if (currentState == BallState.Idle || currentState == BallState.Moving)
        {
            shootMaxCircle.SetActive(true);

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);

            dragStartPos = this.gameObject.transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (currentState == BallState.Idle || currentState == BallState.Moving)
        {
            dragStartPos = this.gameObject.transform.position;

            Vector2 mausePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Vector2 Mause Input
            direction = dragStartPos - mausePos; //Distance between mause and object
            //print("Direction Mag: " + direction.magnitude);


            LineRenderer(mausePos);
        }
    }

    private void OnMouseUp()
    {
        if (currentState == BallState.Idle|| currentState == BallState.Moving)
        {
            shootMaxCircle.SetActive(false);
            lineRenderer.enabled = false;

            if (setMaxSpeed)
            {
                Vector2 directionMax = Vector2.ClampMagnitude(direction, maxSpeed); // Clamp distance
                rigidBody.AddForce(directionMax * power, ForceMode2D.Impulse);
                print("Shoot Power Max: " + directionMax.magnitude * power);
                tryAmount--;
            }
            else
            {
                rigidBody.AddForce(direction * power, ForceMode2D.Impulse);
                print("Shoot Power: " + direction.magnitude * power);
                tryAmount--;
            }
        }
    }

    private void LineRenderer(Vector2 MausePos)
    {
        Vector3 startPos = dragStartPos;
        Vector3 endPos = MausePos;

        Vector3 dir = endPos - startPos;
        float dist = Mathf.Clamp(Vector3.Distance(startPos, endPos), 0, lineRendererMaxRange);
        endPos = startPos + (dir.normalized * dist);

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
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
            //SoundManager.instance.PlaySoundFX(wallHit, .2f);


            GetComponent<Rigidbody2D>().AddForce(new Vector2((direction.x > 0 ? 1 : -1), 
                direction.y > 0 ? 1f : -1f), ForceMode2D.Impulse);
        }

        if (target.tag == "Finish")
        {
            //SoundManager.instance.PlaySoundFX(finishHit, .2f);

            rigidBody.velocity = new Vector2(0, 0);
            currentState = BallState.Finish;
            target.tag = "Untagged";
            gamaManager.CheckFinishState();
            //Win game UI
        }
    }
}
