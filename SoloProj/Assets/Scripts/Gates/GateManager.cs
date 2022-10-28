using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
    public List<Gate> gateList;
    private bool allGatesActivated = false;

    public GameObject levelCompleteScreen;

    public void Start()
    {
        levelCompleteScreen = GameObject.Find("HYPE");
        //levelCompleteScreen.SetActive(false);
    }

    public void GateActivated()
    {
        allGatesActivated = true;

        for(int i = 0; i < gateList.Count; i++)
        {
            if(gateList[i].lit == false)
            {
                allGatesActivated = false;
            }
        }

        // Load Next Level
        if(allGatesActivated == true)
        {
            Debug.Log("Next Level");
            levelCompleteScreen.transform.Find("GameUI").Find("LevelCompletePanelButActually").gameObject.SetActive(true);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
