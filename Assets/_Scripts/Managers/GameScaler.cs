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
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float spriteRatio = background.bounds.size.x / background.bounds.size.y;

        if (screenRatio != spriteRatio)
        {
            float differenceInSize = spriteRatio / screenRatio;
            background.gameObject.transform.localScale = new Vector3(1 * differenceInSize, 1 * differenceInSize, 1);
            stuff.transform.localScale = new Vector3(1 * differenceInSize, 1 * differenceInSize, 1);
            hockey.transform.localScale = new Vector3(1 * differenceInSize, 1 * differenceInSize, 1);

        }



    }
}
