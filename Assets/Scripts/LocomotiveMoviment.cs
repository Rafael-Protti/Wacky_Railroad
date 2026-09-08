using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    public bool isChangingTrack = false; 
    int index = 1;
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
            if(isDrifting) return;
            isDrifting = true;
            
            StartCoroutine("ActivateDrift");

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
        if(!locomotiveResources.RocketAvaliable()) return;

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

        GetComponent<Animator>().SetBool("isTilting", true);
        
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

    public void Direction(bool right)
    {

        if (right)
        {
            GetComponent<Animator>().SetBool("isLeft", false);
            GetComponent<Animator>().SetBool("isRight", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("isRight", false);
            GetComponent<Animator>().SetBool("isLeft", true);
        }

    }

    public void OnGround() // Is invoked in PlayerCollision.cs
    {
        isGrounded = true;
        locomotiveResources.SetRocketValue(10);
    }

    void OnAir()
    {
        StartCoroutine("SetAir");
    }

    IEnumerator SetAir()
    {
        isGrounded = false;
        yield return new WaitForSeconds(1.0f);
        canFall = true;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        rb.MovePosition(Vector3.MoveTowards(transform.position, newPosition, speed * nitroBoost * Time.deltaTime));

        // Vector3 newDirection = target.transform.position - transform.position;
        // newDirection = new Vector3(newDirection.x, 0, newDirection.z);
        // rb.MoveRotation(Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, newDirection, speed * Time.deltaTime, 0.5f * Time.deltaTime)));
    }

    IEnumerator ChangeTrack()
    {
        isChangingTrack = true;

        target = ghosts[index];

        while(isChangingTrack)
        {
            Debug.Log("Changing Track");
            yield return null;
        }

        GetComponent<Animator>().SetBool("isTilting", false);

        Debug.Log("Changed Track!");
    }

    IEnumerator ActivateNitro()
    {
        
        while(locomotiveResources.NitroAvaliable())
        {
            locomotiveResources.SetNitroValue(-0.1f);
            yield return new WaitForSeconds(0.2f);
        }

        SetGhostSpeed(1/nitroBoost);
        speed /= nitroBoost;

        yield return locomotiveResources.StartCoroutine("RecoverNitro");

        isNitro = false;
    }

    IEnumerator ActivateDrift()
    {
        GetComponent<Animator>().SetBool("isDrifting", true);

        while(locomotiveResources.DriftAvaliable())
        {
            locomotiveResources.SetDrifValue(-0.1f);
            yield return new WaitForSeconds(0.2f);
        }

        GetComponent<Animator>().SetBool("isDrifting", false);

        yield return locomotiveResources.StartCoroutine("RecoverDrift");

        isDrifting = false;
    }

    void SetGhostSpeed(float newSpeed)
    {
        for(int index = 0; index < ghosts.Count; index++)
        {
            ghosts[index].GetComponent<Ghost>().ChangeSpeed(newSpeed);
        }
    }
}
