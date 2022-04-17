using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BallScript : MonoBehaviour
{
    private ScoreScript ScoreScriptInstance;
    public static bool isGoal { get; private set; }
    public float maxSpeed;
    private Rigidbody2D rigidBody;

    public AudioClip ballCollision;
    public AudioClip goal;

    public GameObject[] BallColors;

    public bool isVibrating;
    [SerializeField] private long vibrationLong = 30;

    [Header("Particle System")]
    private GameObject particleSys;
    [SerializeField] private GameObject redParticleSystem;
    [SerializeField] private GameObject greenParticleSystem;
    [SerializeField] private GameObject blueParticleSystem;
    [SerializeField] private GameObject yellowParticleSystem;


    [SerializeField]private float particleDelay = .5f;
    private float particleTime;
    private bool canSpawnParticle = false;
    private bool isStuck = false;
    private float stuckCounter = 3;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        ScoreScriptInstance = GameObject.Find("GameManager").GetComponent<ScoreScript>();
        isVibrating = PlayerPrefs.GetInt("Vibrate", 1) == 0;
        isGoal = false;
        particleTime = 0;
    }

    private void Update()
    {
        if (!canSpawnParticle)
        {
            particleTime += Time.deltaTime;
            if (particleTime >= particleDelay)
                canSpawnParticle = true;
        }
        print(rigidBody.velocity.magnitude);
        if (rigidBody.velocity.magnitude <= .1f && stuckCounter > 0)
        {
            stuckCounter -= Time.deltaTime;
            if (stuckCounter <= 0)
            {
                rigidBody.position = new Vector2(0, 0);
                stuckCounter = 3;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (!isGoal)
        {
            if (target.tag == "AIGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                isGoal = true;
                StartCoroutine(ResetBall(false));
                SoundManager.instance.PlaySoundFX(goal, .5f);
                

                if (!EffectManager.instance.isNotVibrating)
                {
                    Vibration.Vibrate(vibrationLong);
                    print("Vibrate");
                }
            }

            else if (target.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                isGoal = true;
                StartCoroutine(ResetBall(true));
                SoundManager.instance.PlaySoundFX(goal, .5f);

                if (!EffectManager.instance.isNotVibrating)
                {
                    Vibration.Vibrate(vibrationLong);
                    print("Vibrate");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        SoundManager.instance.PlaySoundFX(ballCollision, .5f);

        #region Particle Effect
        switch (target.gameObject.tag)
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
            default:
                particleSys = redParticleSystem;
                break;
        }

        if (target.transform.position.x <= -4 && canSpawnParticle)
        {
            Vector3 insPosX = new Vector3(target.transform.position.x, transform.position.y, 1);
            GameObject particle = Instantiate(particleSys, insPosX, Quaternion.Euler(new Vector3(0, 0, -90)));
            particle.GetComponent<ParticleSystem>().Play();
            canSpawnParticle = false;
            particleTime = 0;
        }

        else if (target.transform.position.x >= 4 && canSpawnParticle)
        {
            Vector3 insPosX = new Vector3(target.transform.position.x, transform.position.y, 1);
            GameObject particle = Instantiate(particleSys, insPosX, Quaternion.Euler(new Vector3(0, 0, 90)));
            particle.GetComponent<ParticleSystem>().Play();
            canSpawnParticle = false;
            particleTime = 0;
        }

        else if (target.transform.position.y <= -7 && canSpawnParticle)
        {
            Vector3 insPosY = new Vector3(transform.position.x, target.transform.position.y, 1);
            GameObject particle2 = Instantiate(particleSys, insPosY, Quaternion.identity);
            particle2.GetComponent<ParticleSystem>().Play();
            canSpawnParticle = false;
            particleTime = 0;
        }

        else if (target.transform.position.y >= 7 && canSpawnParticle)
        {
            Vector3 insPosY = new Vector3(transform.position.x, target.transform.position.y, 1);
            GameObject particle2 = Instantiate(particleSys, insPosY, Quaternion.Euler(new Vector3(0, 0, 180)));
            particle2.GetComponent<ParticleSystem>().Play();
            canSpawnParticle = false;
            particleTime = 0;
        }

        else if(canSpawnParticle)
        {
            Vector3 insPosY = new Vector3(transform.position.x, transform.position.y, 1);
            GameObject particle2 = Instantiate(particleSys, insPosY, Quaternion.identity);
            particle2.GetComponent<ParticleSystem>().Play();
            canSpawnParticle = false;
            particleTime = 0;
        }

        #endregion


        #region Light Effect

        if (target.gameObject.tag == "WallRed")
        {

            SetColor(0);
            StartCoroutine(LightEffect(BallColors[0].gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        else if (target.gameObject.tag == "WallGreen")
        {

            SetColor(1);
            StartCoroutine(LightEffect(BallColors[1].gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        else if (target.gameObject.tag == "WallBlue")
        {

            SetColor(2);
            StartCoroutine(LightEffect(BallColors[2].gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        else if (target.gameObject.tag == "WallYellow")
        {
            SetColor(3);
            StartCoroutine(LightEffect(BallColors[3].gameObject, 5, 15, .3f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(0).gameObject, 5, 15, .25f));
            StartCoroutine(LightEffect(target.transform.GetChild(0).GetChild(1).gameObject, 5, 15, .25f));
        }

        else if (target.gameObject.tag == "Player")
        {
            foreach (Transform child in target.transform.GetChild(0))
            {
                if(child.gameObject.activeSelf)
                    StartCoroutine(LightEffect(child.gameObject, 5, 15, .25f));
            }
        }

        else if (target.gameObject.tag == "Player2")
        {
            foreach (Transform child in target.transform.GetChild(0))
            {
                if(child.gameObject.activeSelf)
                    StartCoroutine(LightEffect(child.gameObject, 5, 15, .25f));
            }
        }

        #endregion
    }

    private void SetColor(int Index)
    {
        foreach (var Color in BallColors)
        {
            Color.SetActive(false);
        }
        BallColors[Index].SetActive(true);
    }

    IEnumerator LightEffect(GameObject obj, float intensityValue, float maxIntensityValue, float delay)
    {
        obj.GetComponent<Light2D>().intensity = maxIntensityValue;
        yield return new WaitForSeconds(delay);
        obj.GetComponent<Light2D>().intensity = intensityValue;
    }

    private IEnumerator ResetBall(bool didAiScore)
    {
        yield return new WaitForSeconds(1);
        isGoal = false;

        rigidBody.velocity = rigidBody.position = new Vector2(0, 0);

        if (didAiScore)
            rigidBody.position = new Vector2(0, -1);
        else
            rigidBody.position = new Vector2(0, 1);
    }
}
