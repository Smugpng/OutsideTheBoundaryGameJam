using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapFunctions : MonoBehaviour
{
    public SwapFunctions otherSwap;
    public SwapFunctions thisSwap;
    public Transform playerSpawnPos;
    [Header("Boat")]
    public WaterBoat waterBoat;
    public GameObject boatCam;
    [Header("Player")]
    public GameObject player;
    private void Awake()
    {
        thisSwap = GetComponent<SwapFunctions>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Swap();
        }
    }
    private void Swap()
    {
        if(tag == "Boat")
        {
            otherSwap.enabled = true;
            boatCam.SetActive(false);
            waterBoat.enabled = false;
            player.SetActive(true);
            player.transform.position = playerSpawnPos.position;
            //player.transform.rotation
            thisSwap.enabled = false;
        }
        else if(tag == "Player")
        {
            otherSwap.enabled = true;
            boatCam.SetActive(true);
            waterBoat.enabled = true;
            player.SetActive(false);
            thisSwap.enabled = false;
        }
    }
}
