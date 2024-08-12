using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private Transform curentTransform;
    Vector3 playerDir;
    private GameObject player, boat;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boat = GameObject.FindGameObjectWithTag("Boat");
        
    }

    private void Update()
    {
        if (boat!= null)
        {
            curentTransform = boat.transform;
        }
        else
        {
            curentTransform = player.transform;
        }
        playerDir.z = curentTransform.eulerAngles.y;
        transform.localEulerAngles = playerDir;
    }
}
