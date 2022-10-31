using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;

    public Animator animator;

    public GameObject gameUI;
    private bool isPaused = false;


    Direction dir;
    enum Direction
    {
        Up, Down, Left, Right
    }

    private void Start()
    {
        dir = Direction.Down;
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

            
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if(Input.GetAxisRaw("Horizontal")==1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical")==-1)
            {
                animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                dir = Direction.Right;
                
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                dir = Direction.Left;
            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                dir = Direction.Up;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                dir = Direction.Down;
            }

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
