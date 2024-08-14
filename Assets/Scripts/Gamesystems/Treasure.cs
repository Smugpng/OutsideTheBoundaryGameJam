using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    bool isPickedUp;
    MeshRenderer meshRenderer;
    SphereCollider sphereCollider;
    private float goldGiven;
    private void Awake()
    {
        gameObject.SetActive(true);
        isPickedUp = false;
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        
    }
    private void Start()
    {
        goldGiven = Random.Range(100, 500) * (PlayerPrefs.GetInt("ThreatMultiplier")+1);
        Debug.Log(goldGiven);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isPickedUp)
        {
            Debug.LogWarning(PlayerPrefs.GetFloat("gold"));
            isPickedUp = true;
            meshRenderer.enabled = false;
            float gulp = PlayerPrefs.GetFloat("gold");
            PlayerPrefs.SetFloat("gold", gulp + goldGiven);
            Debug.LogWarning(PlayerPrefs.GetFloat("gold"));
            gameObject.SetActive(false);
        }
    }
}
