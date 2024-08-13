using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;
    CanvasGroup pauseMenu;
    private void Start()
    {
        isPaused = false;
        pauseMenu = GetComponentInChildren<CanvasGroup>();
        pauseMenu.interactable = false;
        pauseMenu.alpha = 0.0f;
    }
    private void Update()
    {
        if  (Input.GetKeyDown(KeyCode.Escape) && isPaused) 
        { 
             UnPause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }

    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.interactable = true;
        pauseMenu.alpha = 1.0f;
        Debug.Log("Stop");
    }
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.interactable = false;
        pauseMenu.alpha = 0.0f;
        Debug.Log("resume");
    }
    public void MainMenu()
    {
        UnPause();
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        UnPause();
        Application.Quit();
    }
}
