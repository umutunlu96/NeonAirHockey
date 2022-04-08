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


    void Start()
    {
        UIManager.Instance.ResetableGameObjects.Add(this);
        rigidBody = GetComponent<Rigidbody2D>();
        ScoreScriptInstance = GameObject.Find("GameManager").GetComponent<ScoreScript>();
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
            }

            else if (target.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                isGoal = true;
                StartCoroutine(ResetBall(true));
                SoundManager.instance.PlaySoundFX(goal, .5f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        SoundManager.instance.PlaySoundFX(ballCollision, .5f);
    }

    private void OnCollisionStay2D(Collision2D target)
    {
        if (target.gameObject.tag == "WallRed" || target.gameObject.tag == "WallGreen" || target.gameObject.tag == "WallBlue" || target.gameObject.tag == "WallYellow")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2((target.transform.position.x > 0 ? -1f : 1f),
            target.transform.position.y > 0 ? -1f : 1f), ForceMode2D.Impulse);
        }
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
        rigidBody.position = new Vector2(0, 0);
    }


}
