using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCount : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float globalTimer = PlayerPrefs.GetFloat("Timer");
        textMeshProUGUI.SetText("Timer: " + Mathf.Round(globalTimer) + "/500" );
    }
}
