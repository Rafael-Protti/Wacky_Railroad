using System;
using UnityEngine;

public class LocomotiveMoviment : MonoBehaviour
{
    [HideInInspector]public bool isGrounded = true; // Is changed in PlayerCollision.cs
    [HideInInspector]public bool inNitro = false;
    public float jumpForce = 500;
    public float fallForce = -500;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if(!isGrounded) return;
        rb.AddForce(new Vector3(0,jumpForce,0));
        isGrounded = false;

        Debug.Log("Jumping");
    }

    public void DriftFall()
    {
        if(isGrounded)
        {
            //Drift
            Debug.Log("Drifting");
            return;
        }
        else
        {  
            rb.AddForce(new Vector3(0,fallForce,0));

            Debug.Log("Falling");
        }
    }

    public void Move(bool right)
    {
        if(right)
        {
            Debug.Log("Going Right");
        }

        else
        {
            Debug.Log("Going Left");
        }
    }

    public void Nitro()
    {
        Debug.Log("Boosting");
    }
}
