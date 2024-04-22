using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class CamPos : MonoBehaviour
{
    private GameObject playerObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");

        }
        else
        {
            this.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y,-10f);
        }

        
    }
}
