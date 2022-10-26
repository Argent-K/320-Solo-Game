using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GateManager : MonoBehaviour
{
    public List<Gate> gateList;
    private bool allGatesActivated = false;



    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
