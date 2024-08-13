using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public CanvasGroup fade;
    public bool startFade = false;
    private void StartGame()
    {
        PlayerPrefs.SetInt("ThreatMultiplier", 0);
        PlayerPrefs.SetFloat("gold",0);
        PlayerPrefs.SetFloat("Timer",0);
        SceneManager.LoadScene(5);
    }
    private void Update()
    {
        if(startFade)
        {
            fade.alpha += Time.deltaTime *2;
            if (fade.alpha >= 1)
            {
                StartGame();
            }
        }
        
    }
    public void Play()
    {
        startFade = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
