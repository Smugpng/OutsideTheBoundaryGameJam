using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldBoundary : MonoBehaviour
{
    public BorderDirection borderDirection;
    private int sceneNumber;
    [SerializeField] private bool isEdge;
    [SerializeField] private bool inStorm;
    public CanvasGroup fade;
    public bool startFade = false;
    public enum BorderDirection
    {
        North,
        East,
        South,
        West,
    }
    private void Start()
    {
        if(inStorm)
        {
            sceneNumber = PlayerPrefs.GetInt("NextScene");
        }
        else
        {
            sceneNumber = SceneManager.GetActiveScene().buildIndex;
        }
 
    }
    private void Update()
    {

        if (startFade)
        {
            fade.alpha += Time.deltaTime/2;
        }
    }

    private void NextScene(BorderDirection borderDirection)
    {
        if (!inStorm)
        {
            startFade = true ;
            PlayerPrefs.SetInt("ThreatMultiplier", +1);
            if (borderDirection == BorderDirection.North)
            {
                if (!isEdge)
                {
                    sceneNumber -= 3;
                }
                else
                {
                    sceneNumber -= (3 * -2);
                }

            }
            else if (borderDirection == BorderDirection.South)
            {
                if (!isEdge)
                {
                    sceneNumber += 3;
                }
                else
                {
                    sceneNumber += (3 * -2);
                }

            }
            else if (borderDirection == BorderDirection.West)
            {
                if (!isEdge)
                {
                    sceneNumber -= 1;
                }
                else
                {
                    sceneNumber -= (1 * -2);
                }

            }
            else if (borderDirection == BorderDirection.East)
            {
                if (!isEdge)
                {
                    sceneNumber += 1;
                }
                else
                {
                    sceneNumber += (1 * -2);
                }

            }
            PlayerPrefs.SetInt("NextScene", sceneNumber);
            Invoke("LoadStormScene", 1f);
        }
        else
        {
            startFade = true;
            Invoke("LoadNextScene", 1f);
        }
        
    }
    private void LoadStormScene()
    {
        SceneManager.LoadScene(10);
    }
    private void LoadNextScene()
    {
        int tempNum = PlayerPrefs.GetInt("NextScene");
        SceneManager.LoadScene(tempNum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boat")
        {
            NextScene(borderDirection);
        }
    }
}
