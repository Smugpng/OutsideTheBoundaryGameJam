using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldBoundary : MonoBehaviour
{
    public BorderDirection borderDirection;
    private int sceneNumber;
    [SerializeField] private bool isEdge;
    [SerializeField] private bool inStorm;
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
            Debug.Log("After strom going to" + sceneNumber);
        }
        else
        {
            sceneNumber = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("Currently In" + sceneNumber);
        }
 
    }
    private void Update()
    {
     
    }

    private void NextScene(BorderDirection borderDirection)
    {
        if (!inStorm)
        {
            PlayerPrefs.SetInt("ThreatMultiplier", +1);
            Debug.Log(PlayerPrefs.GetInt("ThreatMultiplier"));
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
            Debug.Log("Heading into storm to" + sceneNumber);
            PlayerPrefs.SetInt("NextScene", sceneNumber);
            Invoke("LoadStormScene", 1f);
        }
        else
        {
            Debug.Log("Arriving to" + sceneNumber);
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
