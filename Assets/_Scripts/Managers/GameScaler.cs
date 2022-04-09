using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameScaler : MonoBehaviour
{
    public SpriteRenderer background;
    public GameObject stuff;
    public GameObject hockey;

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

            //background.gameObject.transform.localScale = new Vector3(scaleFaxtor, 1, 1);
            stuff.transform.localScale = new Vector3(scaleFaxtor, 1, 1);
            hockey.transform.localScale = new Vector3(.25f+scaleFaxtor, .25f+scaleFaxtor, 1);
        }
    }
}
