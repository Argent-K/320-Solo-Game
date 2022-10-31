using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;

    private bool isGrabbing = false;


    public Animator animator;
    public bool spaceToggle;

    GameObject box;

    // For future updates: https://answers.unity.com/questions/1470694/multiple-tags-for-one-gameobject.html

    private void Start()
    {
        
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;

        

        // Right RayCast
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
        // Left RayCast
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, boxMask);
        // Up RayCast
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x, distance, boxMask);
        // Down RayCast
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.x, distance, boxMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * transform.localScale.x * distance);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * transform.localScale.x * distance);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * transform.localScale.x * distance);

        if (hitRight.collider != null && isGrabbing == false && (hitRight.collider.gameObject.tag == "Pushable" || hitRight.collider.gameObject.tag == "Mirror") && Input.GetKey(KeyCode.Space))
        {
            box = hitRight.collider.gameObject;

            //box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            isGrabbing = true;
            spaceToggle = true;
            animator.SetBool("IsGrabbing", true);
            
        }
        else if (hitLeft.collider != null && isGrabbing == false && (hitLeft.collider.gameObject.tag == "Pushable" || hitLeft.collider.gameObject.tag == "Mirror") && Input.GetKey(KeyCode.Space))
        {
            box = hitLeft.collider.gameObject;

            //box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            isGrabbing = true;
            spaceToggle = true;
            animator.SetBool("IsGrabbing", true);
            animator.SetBool("GrabDown", false);
            animator.SetBool("GrabUp", false);
            animator.SetBool("GrabLeft", true);
        }
        else if (hitUp.collider != null && isGrabbing == false && (hitUp.collider.gameObject.tag == "Pushable" || hitUp.collider.gameObject.tag == "Mirror") && Input.GetKey(KeyCode.Space))
        {
            box = hitUp.collider.gameObject;

            //box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            isGrabbing = true;
            spaceToggle = true;
            animator.SetBool("IsGrabbing", true);
            animator.SetBool("GrabUp", true);
            animator.SetBool("GrabDown", false);
            animator.SetBool("GrabLeft", false);
        }
        else if (hitDown.collider != null && isGrabbing == false && (hitDown.collider.gameObject.tag == "Pushable" || hitDown.collider.gameObject.tag == "Mirror") && Input.GetKey(KeyCode.Space))
        {
            box = hitDown.collider.gameObject;

            //box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            isGrabbing = true;
            spaceToggle = true;
            animator.SetBool("IsGrabbing", true);
            animator.SetBool("GrabDown", true);
            animator.SetBool("GrabUp", false);
            animator.SetBool("GrabLeft", false);

        }
        else if (Input.GetKeyUp(KeyCode.Space) && isGrabbing == true)
        {
            //box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<FixedJoint2D>().connectedBody = default;
            isGrabbing = false;
            spaceToggle = false;
            animator.SetBool("IsGrabbing", false);
            animator.SetBool("GrabDown", false);
            animator.SetBool("GrabUp", false);
            animator.SetBool("GrabLeft", false);
        }

        if(spaceToggle == true)
        {
            animator.SetBool("IsGrabbing", true);
        }
        else
        {
            animator.SetBool("IsGrabbing", false);
            animator.SetBool("GrabDown", false);
            animator.SetBool("GrabUp", false);
            animator.SetBool("GrabLeft", false);
        }


    }

    private void FixedUpdate()
    {
      
    }
}
