using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void GoToMainMenu()
   {
        SceneManager.LoadScene(0);
   }

    private void Start()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        if (currScene > 10)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Invoke("GoToMainMenu", 10);
        }
    }
}
