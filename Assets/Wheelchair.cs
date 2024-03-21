using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelchair : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    private GameObject playerController;
    private Rigidbody rb;

    private void Start()
    {
        playerController = GameObject.Find("FPEPlayerController(Clone)");
        playerController.transform.parent = transform;
    }

    void Update()
    {
        // Forward keys
        if (Input.GetKey(KeyCode.Q)) 
        {
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
            {
                // Forward
                print("Forward");
            }
            else
            {
                // Right
                transform.Rotate(transform.up, Time.deltaTime * turnSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Left
            transform.Rotate(transform.up, Time.deltaTime * -turnSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                // Backward
                print("Backward");
            }
            else
            {
                // Right
                transform.Rotate(transform.up, Time.deltaTime * -turnSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Left
            transform.Rotate(transform.up, Time.deltaTime * turnSpeed);
        }
    }
}

/* ###############################
 * ## Tutorials/Resources Used: ##
 * ###############################
 * 
 * "Unity - Scripting API: Transform.Rotate"
 *     https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
 * 
 * "Unity - Scripting API: Rigidbody.velocity"
 *     https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
 *     
 * "How To Make An Object A Child Of Another Object (SCRIPT) - Questions & Answers - Unity Discussions"
 *     https://discussions.unity.com/t/how-to-make-an-object-a-child-of-another-object-script/94998
*/
