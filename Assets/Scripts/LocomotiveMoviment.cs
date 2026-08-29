using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Analytics;

public class LocomotiveMoviment : MonoBehaviour
{
    [HideInInspector]public bool isGrounded = true; // Is changed in PlayerCollision.cs
    [HideInInspector]public bool ghostCollided = false;
    [HideInInspector]public bool isNitro = false;
    [HideInInspector]public bool isDrifting = false;
    [HideInInspector]public Transform target;
    public List<Transform> ghosts;
    public float speed = 50;
    public float jumpForce = 500;
    public float fallForce = -500;
    int index = 1;
    bool isChangingTrack = false;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        target = ghosts[1];
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
            isDrifting = true;

            Debug.Log("Drifting");
        }
        else
        {  
            rb.AddForce(new Vector3(0,fallForce,0));

            Debug.Log("Falling");
        }
    }

    public void Move(bool right)
    {
        if(isGrounded) return;
        if(isChangingTrack) return;

        ghostCollided = false;

        if(right)
        {
            if(index == 2) return;
            index++;
            Debug.Log("Going Right");
        }

        else
        {
            if(index == 0) return;
            index--;
            Debug.Log("Going Left");
        }

        Debug.Log(index.ToString());
        StartCoroutine("ChangeTrack");
    }

    public void Nitro()
    {
        if(isNitro) return;

        isNitro = true;

        target.GetComponent<Ghost>().speed *= 1.50f;
        speed *= 1.50f;

        StartCoroutine("ActivateNitro");

        Debug.Log("Boosting");
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(target.GetChild(0).transform.position.x, transform.position.y, target.GetChild(0).transform.position.z);
        rb.MovePosition(Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime));
    }

    IEnumerator ChangeTrack()
    {
        isChangingTrack = true;

        target = ghosts[index];

        while(!ghostCollided)
        {
            Debug.Log("Changing Track");
            yield return null;
        }
        Debug.Log("Changed Track!");

        isChangingTrack = false;
    }

    IEnumerator ActivateNitro()
    {
        yield return new WaitForSecondsRealtime(3);

        target.GetComponent<Ghost>().speed /= 1.50f;
        speed /= 1.50f;

        isNitro = false;
    }
}
