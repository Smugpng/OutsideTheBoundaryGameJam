using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    bool isPickedUp;
    MeshRenderer meshRenderer;
    SphereCollider sphereCollider;
    float goldGiven;
    private void Awake()
    {
        isPickedUp = false;
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        goldGiven = Random.Range(100,500) * PlayerPrefs.GetInt("ThreatMultiplier");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isPickedUp)
        {
            isPickedUp = true;
            meshRenderer.enabled = false;
            float gulp = PlayerPrefs.GetFloat("gold");
            PlayerPrefs.SetFloat("gold", gulp+goldGiven);
        }
    }
}
