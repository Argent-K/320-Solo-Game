using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    // Passed variable:
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    GateManager gateManager;
    SpriteRenderer spriteRenderer;
    public bool lit;
    void Start()
    {
        lit = false;

        // Finds the gameObject with tag of GateManager
        gateManager = GameObject.FindGameObjectsWithTag("GateManager")[0].GetComponent<GateManager>();
        gateManager.gateList.Add(this);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Make sure to set the lit state to true
    void OnHitEnter()
    {
        Debug.Log("OnHitEnter");
        lit = true;
        spriteRenderer.sprite = activatedSprite;
        gateManager.GateActivated();
    }

    // Check to see if lit state is still true
    void OnHitStay()
    {
        Debug.Log("OnHitStay");
        if (lit == false)
        {
            lit = true;
            spriteRenderer.sprite = activatedSprite;
        }
        
    }

    void OnHitExit()
    {
        Debug.Log("OnHitExit");
        lit = false;
        spriteRenderer.sprite = deactivatedSprite;
    }

}
