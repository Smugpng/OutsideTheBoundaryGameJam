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
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) 
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnPause();
        }

    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
        isPaused =true;
        pauseMenu.interactable = true;
        pauseMenu.alpha = 1.0f;
        
    }
    public void UnPause()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenu.interactable = false;
        pauseMenu.alpha = 0.0f;
        
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }
}
