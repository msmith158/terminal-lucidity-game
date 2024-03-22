using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelchair : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSmoothAmount;
    [SerializeField] private float turnSpeed;

    private GameObject playerController;
    private Rigidbody rb;

    private float realMoveSpeed;
    private float realTurnSpeed;
    private float timeElapsed;

    private void Start()
    {
        playerController = GameObject.Find("FPEPlayerController(Clone)");
        playerController.transform.parent = transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Forward keys
        if (Input.GetKey(KeyCode.Q)) 
        {
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
            {
                // Forward
                if (timeElapsed < moveSmoothAmount)
                {
                    rb.velocity = transform.forward * Mathf.Lerp(0, -moveSpeed, timeElapsed / moveSmoothAmount);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    rb.velocity = transform.forward * -moveSpeed;
                }
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
                // Backwards
                if (timeElapsed < moveSmoothAmount)
                {
                    rb.velocity = transform.forward * Mathf.Lerp(0, moveSpeed, timeElapsed / moveSmoothAmount);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    rb.velocity = transform.forward * moveSpeed;
                }
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

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            timeElapsed = 0;
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
 *     
 * "Unity - Scripting API: Rigidbody.velocity"
 *     https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
 *     
 * "Velocity based on rotation - Unity Forum"
 *     https://forum.unity.com/threads/velocity-based-on-rotation.32500/
 *     
 * "Unity - Scripting API: Rigidbody.constraints"
 *     https://docs.unity3d.com/ScriptReference/Rigidbody-constraints.html
 *     
 * "The right way to Lerp in Unity (with examples) - Game Dev Beginner"
 *     https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
*/
