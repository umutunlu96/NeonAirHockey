using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<PlayerMovement> Players = new List<PlayerMovement>();

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

            foreach (var Player in Players)
            {
                if (Player.LockedFingerID == null)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began && Player.PlayerCollider.OverlapPoint(touchWorldPos))
                    {
                        Player.LockedFingerID = Input.GetTouch(i).fingerId;
                    }
                }
                else if (Player.LockedFingerID == Input.GetTouch(i).fingerId)
                {
                    Player.MoveToPosition(touchWorldPos);
                    if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Canceled)
                    {
                        Player.LockedFingerID = null;
                    }
                }
            }
        }
    }
}
