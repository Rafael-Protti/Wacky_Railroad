using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Splines;

public class LocomotiveMoviment : MonoBehaviour
{
    [HideInInspector]public bool isGrounded = true;
    [HideInInspector]public bool isNitro = false;
    [HideInInspector]public bool isDrifting = false;
    [HideInInspector]public bool canFall = false;
    public List<Transform> ghosts;
    Transform target;
    public float speed = 50f;
    public float rocketSpeed = 50f;
    public float nitroBoost = 2f;
    public float jumpForce = 5f;
    public float fallForce = -5f;
    int index = 1;
    bool isChangingTrack = false; 
    Rigidbody rb;
    LocomotiveResources locomotiveResources;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        locomotiveResources = GetComponent<LocomotiveResources>();
        target = ghosts[1];
        SetGhostSpeed(speed);
    }

    public void Jump()
    {
        if(!isGrounded) return;

        rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
        OnAir();

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
            //Fall
            if(!canFall) return;

            rb.AddForce(new Vector3(0,fallForce,0), ForceMode.Impulse);

            canFall = false;

            Debug.Log("Falling");
        }
    }

    public void Move(bool right)
    {
        if(isGrounded) return;
        if(isChangingTrack) return;

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

        locomotiveResources.SetRocketValue(-1f);
        
        Debug.Log("Target:" + index.ToString());
        StartCoroutine("ChangeTrack");
    }

    public void Nitro()
    {
        if(isNitro) return;

        isNitro = true;

        SetGhostSpeed(nitroBoost);
        speed *= nitroBoost;

        StartCoroutine("ActivateNitro");

        Debug.Log("Boosting");
    }

    public void OnGround() // Is invoked in PlayerCollision.cs
    {
        isGrounded = true;
        locomotiveResources.SetRocketValue(1f);
    }

    public void OnAir() // Is invoked in PlayerCollision.cs
    {
        isGrounded = false;
        canFall = true;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        rb.MovePosition(Vector3.MoveTowards(transform.position, newPosition, speed * nitroBoost * Time.deltaTime));
    }

    IEnumerator ChangeTrack()
    {
        isChangingTrack = true;

        target = ghosts[index];

        while(!isGrounded)
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


        SetGhostSpeed(1/nitroBoost);
        speed /= nitroBoost;

        isNitro = false;
    }

    void SetGhostSpeed(float newSpeed)
    {
        for(int index = 0; index < ghosts.Count; index++)
        {
            ghosts[index].GetComponent<Ghost>().ChangeSpeed(newSpeed);
        }
    }
}
