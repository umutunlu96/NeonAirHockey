using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameScaler : MonoBehaviour
{
    public SpriteRenderer background;
    public GameObject stuff;
    public GameObject hockey;
    public GameObject hockey2;
    public GameObject ball;


    [SerializeField] private float hockeyOffset;
    [SerializeField] private float ballOffset;
    void Update()
    {
        float screenRatioX = (float)Screen.width;
        float screenRatioY = (float)Screen.height;

        float spriteRatioX = background.bounds.size.x;
        float spriteRatioY = background.bounds.size.y;

        if (spriteRatioX != screenRatioX || spriteRatioY != screenRatioY)
        {
            float ratioX = spriteRatioX / screenRatioX;
            float ratioY = spriteRatioY / screenRatioY;
            float scaleFaxtor = ratioY / ratioX;

            stuff.transform.localScale = new Vector3(scaleFaxtor, 1, 1);

            hockey.transform.localScale = new Vector3(hockeyOffset + scaleFaxtor, hockeyOffset + scaleFaxtor, 1);

            if(hockey2 != null)
                hockey2.transform.localScale = new Vector3(hockeyOffset + scaleFaxtor, hockeyOffset + scaleFaxtor, 1);

            if (ball != null)
                ball.transform.localScale = new Vector3(ballOffset + scaleFaxtor, ballOffset + scaleFaxtor, 1);
        }
    }
}
