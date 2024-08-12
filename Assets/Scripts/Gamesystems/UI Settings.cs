using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettings : MonoBehaviour
{

    [SerializeField]
    private GameObject compass, ammouCount,player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!player.activeInHierarchy)
        {
            ammouCount.SetActive(false);
            compass.SetActive(true);
        }
        else
        {
            ammouCount.SetActive(true);
            compass.SetActive(false);
        }
      
    }
}
