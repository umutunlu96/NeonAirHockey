using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Hockey : MonoBehaviour
{
    public AudioClip wallHit;
    //public AudioClip obsHit;
    public AudioClip finishHit;

    public float power = 5;
    public float lineRendererMaxRange = 3;

    public bool setMaxSpeed = false;
    public float maxSpeed = 5;

    private GameManager gamaManager;


    private GameObject shootMaxCircle;
    private LineRenderer lineRenderer;


    private Vector2 direction;
    private Vector2 dragStartPos;

    private Rigidbody2D rigidBody;
    private GameObject colorChilds;

    [SerializeField] private long vibrationLong = 30;

    [Header("Particle System")]
    private GameObject particleSys;
    [SerializeField] private GameObject redParticleSystem;
    [SerializeField] private GameObject greenParticleSystem;
    [SerializeField] private GameObject blueParticleSystem;
    [SerializeField] private GameObject yellowParticleSystem;

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

    private void Start()
    {
        lineRenderer.enabled = false;
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

        if (rigidBody.velocity.magnitude < .15f && currentState != BallState.Finish)
            rigidBody.velocity *= rigidBody.velocity * .95f;
        //print(rigidBody.velocity.magnitude);
    }

    private void OnMouseDown()
    {
        if (currentState == BallState.Idle || currentState == BallState.Moving)
        {
            shootMaxCircle.SetActive(true);

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);

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
            }
            else
            {
                rigidBody.AddForce(direction * power, ForceMode2D.Impulse);
                print("Shoot Power: " + direction.magnitude * power);
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
            SoundManager.instance.PlaySoundFX(wallHit, .2f);

            SetActiveChildren(0);
            StartCoroutine(LightEffect(colorChilds.transform.GetChild(0).gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        if (target.gameObject.tag == "WallGreen")
        {
            SoundManager.instance.PlaySoundFX(wallHit, .2f);

            SetActiveChildren(1);
            StartCoroutine(LightEffect(colorChilds.transform.GetChild(1).gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        if (target.gameObject.tag == "WallBlue")
        {
            SoundManager.instance.PlaySoundFX(wallHit, .2f);

            SetActiveChildren(2);
            StartCoroutine(LightEffect(colorChilds.transform.GetChild(2).gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        if (target.gameObject.tag == "WallYellow")
        {
            SoundManager.instance.PlaySoundFX(wallHit, .2f);

            SetActiveChildren(3);
            StartCoroutine(LightEffect(colorChilds.transform.GetChild(3).gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        if (target.tag == "Finish")
        {
            SoundManager.instance.PlaySoundFX(finishHit, .2f);
            Vibration.Vibrate(vibrationLong);
            rigidBody.velocity = new Vector2(0, 0);
            currentState = BallState.Finish;
            target.tag = "Untagged";
            gamaManager.CheckFinishState();
            //Win game UI
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isVibrating)
        {
            Vibration.Vibrate(vibrationLong);
        }

        switch (collision.gameObject.tag)
        {
            case "WallRed":
                particleSys = redParticleSystem;
                break;
            case "WallGreen":
                particleSys = greenParticleSystem;
                break;
            case "WallBlue":
                particleSys = blueParticleSystem;
                break;
            case "WallYellow":
                particleSys = yellowParticleSystem;
                break;
            case "ObsRed":
                particleSys = redParticleSystem;
                break;
            case "ObsGreen":
                particleSys = greenParticleSystem;
                break;
            case "ObsBlue":
                particleSys = blueParticleSystem;
                break;
            case "ObsYellow":
                particleSys = yellowParticleSystem;
                break;
        }

        if (collision.transform.position.x <= -3)
        {
            Vector3 insPosX = new Vector3(collision.transform.position.x,transform.position.y,1);
            GameObject particle = Instantiate(particleSys, insPosX, Quaternion.Euler(new Vector3(0, 0, -90)));
            particle.GetComponent<ParticleSystem>().Play();
        }

        if (collision.transform.position.x >= 3)
        {
            Vector3 insPosX = new Vector3(collision.transform.position.x, transform.position.y, 1);
            GameObject particle = Instantiate(particleSys, insPosX, Quaternion.Euler(new Vector3(0, 0, 90)));
            particle.GetComponent<ParticleSystem>().Play();
        }


        if (collision.transform.position.y <= -5)
        {
            Vector3 insPosY = new Vector3(transform.position.x, collision.transform.position.y, 1);
            GameObject particle2 = Instantiate(particleSys, insPosY, Quaternion.identity);
            particle2.GetComponent<ParticleSystem>().Play();
        }

        if (collision.transform.position.y >= 5 && collision.gameObject.tag != "Kale")
        {
            Vector3 insPosY = new Vector3(transform.position.x, collision.transform.position.y, 1);
            GameObject particle2 = Instantiate(particleSys, insPosY, Quaternion.Euler(new Vector3(0, 0, 180)));
            particle2.GetComponent<ParticleSystem>().Play();
        }

        if (collision.gameObject.tag == "Kale")
        {
            SoundManager.instance.PlaySoundFX(wallHit, .2f);
            StartCoroutine(LightEffect(colorChilds.transform.GetChild(3).gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(collision.transform.GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(collision.transform.GetChild(1).gameObject, 5, 15, .25f));
        }
    }

    private void SetActiveChildren(int childIndex)
    {
        foreach (Transform child in colorChilds.transform)
        {
            child.gameObject.SetActive(false);
        }
        colorChilds.transform.GetChild(childIndex).gameObject.SetActive(true);
    }

    IEnumerator LightEffect (GameObject obj, float intensityValue, float maxIntensityValue, float delay)
    {
        obj.GetComponent<Light2D>().intensity = maxIntensityValue;
        yield return new WaitForSeconds(delay);
        obj.GetComponent<Light2D>().intensity = intensityValue;
    }
}
