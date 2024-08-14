using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCount : MonoBehaviour
{
    Image health;
    void Start()
    {
        health = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        health.fillAmount = PlayerPrefs.GetFloat("PlayerCurhealth")/10;
    }
}
