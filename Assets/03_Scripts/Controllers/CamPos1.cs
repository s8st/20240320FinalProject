using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPos1 : MonoBehaviour
{
    private GameObject playerObj;
    private CinemachineVirtualCamera followTarget;

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
            followTarget = transform.GetComponent<CinemachineVirtualCamera>();
            followTarget.Follow = playerObj.transform;

        }
        else
        {

            //this.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y,-10f);
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y,-10f);
        }

        
    }
}
