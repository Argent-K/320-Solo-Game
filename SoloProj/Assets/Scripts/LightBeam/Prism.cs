using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Note for prism child hierarchy
// The first child of the emptyPrism gameObject needs to be the lightSpawn location
// Initial light spawns shooting up, but can be changed in code to other directions
// the lightSpawn location also holds the lineRenderer component

// https://www.reddit.com/r/Unity2D/comments/o9vzub/how_do_i_make_a_line_renderer_have_collision/ Line renderer Collisions
// https://medium.com/c-sharp-progarmming/make-a-basic-fsm-in-unity-c-f7d9db965134 Finite State Machine
// @Sean on discord to tell him to menu
// 

public class Prism : MonoBehaviour
{
    // Reference to the lineRenderer used to draw the light beam
    private LineRenderer lineRenderer;

    // Variables that control how far the laser can travel
    private const float MAX_LASER_DIST = 30.0f;
    private const int MAX_BOUNCES = 50;

    // Variables that control the LineRenderer's position list.
    private int lrIndex = 0;
    private int lrCount = 0;

    // Reference to the GateManager
    GateManager gateManager;

    // Reference to the gate that the laser previously hit
    private GameObject hitObject;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the lineRenderer attached to object and makes sure it uses worldSpace
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        // init gatemanager
        gateManager = GameObject.FindGameObjectsWithTag("GateManager")[0].GetComponent<GateManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Every frame calculates and emits light beam
        EmitBeam(transform.GetChild(0).position, transform.up);
        // Reseting lineRenderer position list so that we avoid stackoverflow error
        lrIndex = 0;
        lrCount = 0;
    }

    /// <summary>
    /// Renders and Calculates where a beam of light will go and bounce in one instance
    /// </summary>
    /// <param name="position">Initial position from where the light beam will start from</param>
    /// <param name="direction">The direction that the light beam will travel</param>
    void EmitBeam(Vector2 position, Vector2 direction)
    {
        // Sets lineRenderers position list to contain the initial point where the light
        // beam will start from
        lrCount = 1;
        lineRenderer.positionCount = lrCount;
        lineRenderer.SetPosition(0, transform.GetChild(0).position + Vector3.forward);

        // Loops through light calculation for a specified number of bounces to prevent infinite bounces from occuring
        for (int i = 0; i < MAX_BOUNCES; i++)
        {
            lrCount++;
            lrIndex++;

            // Casts a ray in specified direction for MAX_LASER_DIST
            RaycastHit2D hit = Physics2D.Raycast(position, direction, MAX_LASER_DIST, 1);
            Ray2D ray = new Ray2D(position, direction);

            if (hit.collider == null)
            {
                // Light beam didn't collide with anyObject, sets lineRenderer to MAX_LASER_DIST
                lineRenderer.positionCount = lrCount;
                lineRenderer.SetPosition(lrIndex, ray.GetPoint(MAX_LASER_DIST));

                // Check to see if hitObject has been registered
                if(hitObject != null)
                {
                    // Might have to change gatePrefab to have gate script on sprite
                    hitObject.SendMessage("OnHitExit");
                    hitObject = null;
                }

                break;
            }
            else if(hit.collider.tag != "Mirror" && hit.collider.tag != "Gate")
            {
                // Light beam collides with a collider thats not a mirror or a gate
                lineRenderer.positionCount = lrCount;
                lineRenderer.SetPosition(lrIndex, hit.point);

                if(hitObject != null)
                {
                    hitObject.SendMessage("OnHitExit");
                    hitObject = null;
                }
                break;
            }
            else if(hit.collider.tag == "Mirror")
            {
                // Light beam collides with a collider with the mirror tag
                lineRenderer.positionCount = lrCount;
                lineRenderer.SetPosition(lrIndex, hit.point);

                // Reflect the direction of the Light Beam
                direction = Vector2.Reflect(direction, hit.normal);
                // Sets starting position of the reflected light beam to have an offset of the mirror
                // May cause light to not reflect perfectly but can change the multiplier of the direction to maybe solve this issue
                position = hit.point - ray.direction * 0.01f;
                
                
            }
            else if(hit.collider.tag == "Gate")
            {
                // Light beam hits a gate and now will send a debug msg that it has hit a gate.
                lineRenderer.positionCount = lrCount;
                lineRenderer.SetPosition(lrIndex, hit.point);

                GameObject go = hit.transform.gameObject;

                // The lightbeam hasn't hit anything yet
                if (hitObject == null)
                {
                    go.SendMessage("OnHitEnter");
                }
                // Check to se if the hit object is the same as the registered object
                else if(hitObject.GetInstanceID() == go.GetInstanceID())
                {
                    hitObject.SendMessage("OnHitStay");
                }
                // Hits new object but not the same object
                else
                {
                    hitObject.SendMessage("OnHitExit");
                    go.SendMessage("OnHitEnter");
                }

                hitObject = go;



                //hitObject = hit.collider.GetComponentInParent<GameObject>();
                //
                //hit.collider.GetComponentInParent<Gate>().lit = true;
                //gateManager.GateActivated();




                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            }
        }

    }

}
