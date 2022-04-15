using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour, IResettable
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

    void Start()
    {
        UIManager.Instance.ResetableGameObjects.Add(this);
        rigidBody = GetComponent<Rigidbody2D>();
        ScoreScriptInstance = GameObject.Find("GameManager").GetComponent<ScoreScript>();
        isVibrating = PlayerPrefs.GetInt("Vibrate", 1) == 0;
        isGoal = false;
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
                
                if(isVibrating)
                    Vibration.Vibrate(vibrationLong);
            }

            else if (target.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                isGoal = true;
                StartCoroutine(ResetBall(true));
                SoundManager.instance.PlaySoundFX(goal, .5f);
                    
                if(isVibrating)
                    Vibration.Vibrate(vibrationLong);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        SoundManager.instance.PlaySoundFX(ballCollision, .5f);

        if (target.gameObject.tag == "WallRed")
            SetColor(0);
        if (target.gameObject.tag == "WallGreen")
            SetColor(1);
        if (target.gameObject.tag == "WallBlue")
            SetColor(2);
        if (target.gameObject.tag == "WallYellow")
            SetColor(3);
    }

    private void SetColor(int Index)
    {
        foreach (var Color in BallColors)
        {
            Color.SetActive(false);
        }
        BallColors[Index].SetActive(true);
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

    public void ResetPosition()
    {
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.position = new Vector2(0, 0);
    }
}
