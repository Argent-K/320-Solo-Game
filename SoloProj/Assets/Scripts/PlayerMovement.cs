using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;

    public GameObject gameUI;
    private bool isPaused = false;
    private void Start()
    {
        gameUI = GameObject.Find("HYPE");
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        if(!isPaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Opens pause menu   
            // Set isPaused to true, unless esc is pressed again
            if(isPaused)
            {
                isPaused = false;
                gameUI.transform.Find("GameUI").Find("PauseMenuButActually").gameObject.SetActive(false);
            }
            else
            {
                isPaused = true;
                movement.x = 0;
                movement.y = 0;
                gameUI.transform.Find("GameUI").Find("PauseMenuButActually").gameObject.SetActive(true);
            }
            
        }
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void UnPause()
    {
        isPaused = false;
        gameUI.transform.Find("GameUI").Find("PauseMenuButActually").gameObject.SetActive(false);
    }
}
