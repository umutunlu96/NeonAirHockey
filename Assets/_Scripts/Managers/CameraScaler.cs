using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    public SpriteRenderer background;


    private void Awake()
    {
        //background = GameObject.Find("CameraScale").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = background.bounds.size.x / background.bounds.size.y;

        if (screenRatio > targetRatio)
        {
            Camera.main.orthographicSize = background.bounds.size.y / 2;
        }

        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = background.bounds.size.y / 2 * differenceInSize;
        }

    }
}


//[ExecuteInEditMode]
//[RequireComponent(typeof(Camera))]
//public class CameraScaler : MonoBehaviour
//{
//    public SpriteRenderer background;

//    private void Update()
//    {
//        if (background == null)
//        {
//            background = GameObject.Find("CameraScale").GetComponent<SpriteRenderer>();
//        }
//        float ortoSize = background.bounds.size.x * Screen.height / Screen.width * .5f;
//        Camera.main.orthographicSize = ortoSize;
//    }
//}
