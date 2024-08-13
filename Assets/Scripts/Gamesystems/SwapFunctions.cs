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
    private PlayerHealth ph;
    private PlayerController pc;
    public GameObject player;
    public LayerMask boat;
    private void Awake()
    {
        thisSwap = GetComponent<SwapFunctions>();
        if(this.tag == "Player")
        {
            ph = GetComponent<PlayerHealth>();
            pc = GetComponent<PlayerController>();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(tag == "Player")
            {
               bool boatNear = Physics.CheckSphere(transform.position, 10, boat);
                if (!boatNear) return;
            }
            Swap();
        }
    }
    private void Swap()
    {
        Rigidbody rb = waterBoat.gameObject.GetComponent<Rigidbody>();

        if(tag == "Boat")
        {
            otherSwap.enabled = true;
            boatCam.SetActive(false);
            waterBoat.enabled = false;
            player.SetActive(true);
            //player.transform.rotation
            thisSwap.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            player.transform.position = playerSpawnPos.position;

        }
        else if(tag == "Player")
        {
            pc.inWater = false;
            ph.drownTimer = 0;
            rb.constraints = RigidbodyConstraints.None;
            otherSwap.enabled = true;
            boatCam.SetActive(true);
            waterBoat.enabled = true;
            player.SetActive(false);
            thisSwap.enabled = false;
        }
    }
}
