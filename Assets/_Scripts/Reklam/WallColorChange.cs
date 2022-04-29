using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        int randomInt = Random.Range(0, 4);

        if (target.gameObject.tag == "WallRed" || target.gameObject.tag == "WallGreen" || target.gameObject.tag == "WallBlue" || 
            target.gameObject.tag == "WallYellow" || target.gameObject.tag == "PlayerGoal" || target.gameObject.tag == "AIGoal")
        {
            target.gameObject.SetActive(false);
            target.transform.parent.transform.GetChild(randomInt).gameObject.SetActive(true);

        }
    }


}
