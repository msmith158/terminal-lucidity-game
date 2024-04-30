using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelchairGPTEdit : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 2)] private float moveSmoothAmount;
    [SerializeField] private float turnSpeed;
    [SerializeField, Range(0, 1)] private float turnSmoothAmount;

    private GameObject playerController;
    private Rigidbody rb;

    private float realMoveSpeed;
    private float realTurnSpeed;
    private float moveTimeElapsed;
    private float turnTimeElapsed;

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
                RotateWheelchair(1);
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Left
            RotateWheelchair(-1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
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
            moveTimeElapsed = 0;
        }
    }

    void RotateWheelchair(int direction)
    {
        if (turnTimeElapsed < turnSmoothAmount)
        {
            // Calculate the current step of rotation
            float currentStep = Mathf.Lerp(0, turnSpeed * direction, turnTimeElapsed / turnSmoothAmount);
            // Rotate around the Y axis at the calculated step
            transform.Rotate(0, currentStep * Time.deltaTime, 0, Space.World);
            // Increment the time elapsed
            turnTimeElapsed += Time.deltaTime;
        }
        else
        {
            // If reached or exceeded the smooth amount time, rotate at full speed
            transform.Rotate(0, turnSpeed * direction * Time.deltaTime, 0, Space.World);
        }
    }
}