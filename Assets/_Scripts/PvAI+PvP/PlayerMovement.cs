using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IResettable
{
    Rigidbody2D rigidBody;

    public Transform BoundaryHolder;

    public Collider2D PlayerCollider { get; private set; }

    Boundary playerBoundary;

    private Vector2 startingPosition;

    private PlayerController playerController;

    public int? LockedFingerID { get; set; }

    private void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        startingPosition = rigidBody.position;

        PlayerCollider = GetComponent<Collider2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }

    private void OnEnable()
    {
        playerController.Players.Add(this);

        UIManager.Instance.ResetableGameObjects.Add(this);
    }

    private void OnDisable()
    {
        playerController.Players.Remove(this);
        
        UIManager.Instance.ResetableGameObjects.Remove(this);
    }

    public void MoveToPosition(Vector2 position)
    {
        Vector2 clampedMousePos = new Vector2(Mathf.Clamp(position.x, playerBoundary.Left, playerBoundary.Right),
                                      Mathf.Clamp(position.y, playerBoundary.Down, playerBoundary.Up));
        rigidBody.MovePosition(clampedMousePos);
    }

    public void ResetPosition()
    {
        rigidBody.position = startingPosition;
    }


}
