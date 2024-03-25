using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelchair : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 2)] private float moveSmoothAmount;
    [SerializeField] private float turnSpeed;
    [SerializeField, Range(0, 1)] private float turnSmoothAmount;

    private GameObject playerController;
    private Rigidbody rb;

    private float realTurnSpeed;
    private float moveTimeElapsed;
    private float turnTimeElapsed;
    private float turnTimeElapsed2;
    //private bool isKeyPressed = false;

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
            //isKeyPressed = true;
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
            {
                // Forward
                if (moveTimeElapsed < moveSmoothAmount)
                {
                    rb.velocity = transform.forward * Mathf.Lerp(0, -moveSpeed, moveTimeElapsed / moveSmoothAmount);
                    moveTimeElapsed += Time.deltaTime;
                }
                else
                {
                    rb.velocity = transform.forward * -moveSpeed;
                }
            }
            else
            {
                // Right
                if (turnTimeElapsed < turnSmoothAmount)
                {
                    realTurnSpeed = Mathf.Lerp(0, turnSpeed, turnTimeElapsed / turnSmoothAmount);
                    turnTimeElapsed += Time.deltaTime;
                }
                else
                {
                    realTurnSpeed = turnSpeed;
                }
                transform.Rotate(transform.up, Time.deltaTime * realTurnSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            //isKeyPressed = true;
            // Left
            if (turnTimeElapsed < turnSmoothAmount)
            {
                realTurnSpeed = Mathf.Lerp(0, -turnSpeed, turnTimeElapsed / turnSmoothAmount);
                turnTimeElapsed += Time.deltaTime;
            }
            else
            {
                realTurnSpeed = -turnSpeed;
            }
            transform.Rotate(transform.up, Time.deltaTime * realTurnSpeed);
        }
        // Backward keys
        else if (Input.GetKey(KeyCode.A))
        {
            //isKeyPressed = true;
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                // Backwards
                if (moveTimeElapsed < moveSmoothAmount)
                {
                    rb.velocity = transform.forward * Mathf.Lerp(0, moveSpeed, moveTimeElapsed / moveSmoothAmount);
                    moveTimeElapsed += Time.deltaTime;
                }
                else
                {
                    rb.velocity = transform.forward * moveSpeed;
                }
            }
            else
            {
                // Right
                if (turnTimeElapsed < turnSmoothAmount)
                {
                    realTurnSpeed = Mathf.Lerp(0, -turnSpeed, turnTimeElapsed / turnSmoothAmount);
                    turnTimeElapsed += Time.deltaTime;
                }
                else
                {
                    realTurnSpeed = -turnSpeed;
                }
                transform.Rotate(transform.up, Time.deltaTime * realTurnSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //isKeyPressed = true;
            // Left
            if (turnTimeElapsed < turnSmoothAmount)
            {
                realTurnSpeed = Mathf.Lerp(0, turnSpeed, turnTimeElapsed / turnSmoothAmount);
                turnTimeElapsed += Time.deltaTime;
            }
            else
            {
                realTurnSpeed = turnSpeed;
            }
            transform.Rotate(transform.up, Time.deltaTime * realTurnSpeed);
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            // Couldn't figure this one out
            /*if (Input.GetKeyUp(KeyCode.Q) && turnTimeElapsed == turnSmoothAmount)
            {
                if (!Input.GetKey(KeyCode.Q))
                {
                    if (turnTimeElapsed < turnSmoothAmount)
                    {
                        realTurnSpeed = Mathf.Lerp(realTurnSpeed, 0, turnTimeElapsed / turnSmoothAmount);
                        turnTimeElapsed += Time.deltaTime;
                    }
                    else
                    {
                        realTurnSpeed = 0;
                    }
                    transform.Rotate(transform.up, Time.deltaTime * realTurnSpeed);
                }
            }*/

            /*if (isKeyPressed == true)
            {
                StartCoroutine(StopRotation(realTurnSpeed));
                isKeyPressed = false;
            }*/
            moveTimeElapsed = 0;
            turnTimeElapsed = 0;
        }
    }

    /*IEnumerator StopRotation(float speed)
    {
        if (turnTimeElapsed < turnSmoothAmount)
        {
            realTurnSpeed = Mathf.Lerp(speed, 0, turnTimeElapsed / turnSmoothAmount);
            turnTimeElapsed += Time.deltaTime;
        }
        else
        {
            realTurnSpeed = 0;
        }
        transform.Rotate(transform.up, Time.deltaTime * realTurnSpeed);

        yield return new WaitForSeconds(turnTimeElapsed);


    }*/
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
