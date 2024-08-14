using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainObj : MonoBehaviour
{
    public float goldEarned = 0;
    public float globalTimer = 0;
    protected float goldNeededtoWin = 5000;

    private void Start()
    {
        goldEarned = PlayerPrefs.GetFloat("gold");
        globalTimer = PlayerPrefs.GetFloat("Timer");
    }
    private void Update()
    {
        goldEarned = PlayerPrefs.GetFloat("gold");
        globalTimer += Time.deltaTime;
        PlayerPrefs.SetFloat("Timer",globalTimer);
        if(globalTimer > 300)
        { 
            SceneManager.LoadScene(11);
        }
        if(goldEarned >= goldNeededtoWin)
        {

            SceneManager.LoadScene(12);
        }
    }
    
}
