using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    GateManager gateManager;
    public bool lit;
    void Start()
    {
        lit = false;

        // Finds the gameObject with tag of GateManager
        gateManager = GameObject.FindGameObjectsWithTag("GateManager")[0].GetComponent<GateManager>();
        gateManager.gateList.Add(this);

        
    }

}
