using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObj : MonoBehaviour
{
    public float goldEarned = 0;
    public float globalTimer = 0;
    public float goldNeededtoWin;

    private void Start()
    {
        goldEarned += PlayerPrefs.GetFloat("gold");
        globalTimer += PlayerPrefs.GetFloat("Timer");
    }
    private void Update()
    {
        globalTimer += Time.deltaTime;
        PlayerPrefs.SetFloat("Timer",globalTimer);
        if(globalTimer >= 300)
        {
            Debug.LogWarning("times up");
           //EndGame();
        }
        if(goldEarned >= goldNeededtoWin)
        {
            //WinGame
        }
    }
    public void EarnGold()
    {
        goldEarned += 100;
        PlayerPrefs.SetFloat("gold", goldEarned);
    }
}
