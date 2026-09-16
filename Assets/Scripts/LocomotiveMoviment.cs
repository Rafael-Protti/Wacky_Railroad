using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    public List<int> points = new List<int>{-5,0,5};
    public float speed = 5f;
    public float acceleration = 5f;
    public float maxSpeed = 5f;
    public float rocketSpeed = 50f;
    public float nitroBoost = 2f;
    public float jumpForce = 5f;
    public float fallForce = 5f;
    public float gravityForce = 5f;
    public bool isChangingTrack = false; 
    public Transform jumpMaxPoint;
    int direction = -1;
    int index = 1;
    Rigidbody rb;
    LocomotiveResources locomotiveResources;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        locomotiveResources = GetComponent<LocomotiveResources>();
    }

    public void Jump()
    {
        if(!isGrounded) return;

        rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
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

            rb.AddForce(Physics.gravity * gravityForce * fallForce  * Time.fixedDeltaTime, ForceMode.VelocityChange);

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
            direction = 1;
            index++;        
            Debug.Log("Going Right");
        }

        else
        {
            if(index == 0) return;
            direction = -1;
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

        speed *= nitroBoost;
        maxSpeed *= nitroBoost;

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
        rb.maxLinearVelocity = maxSpeed;
        
        if(transform.position.y >= jumpMaxPoint.transform.position.y)
        {
            float pullStrenght = Mathf.Abs(Physics.gravity.y) * gravityForce * Time.fixedDeltaTime;
            Vector3 pullForce = Vector3.Lerp(transform.position, Vector3.down, acceleration * Time.fixedDeltaTime);
            rb.AddForce(pullForce * pullStrenght, ForceMode.VelocityChange);
        }

        Vector3 next = Vector3.forward * speed * Time.fixedDeltaTime;
        Vector3 current = new Vector3(0, 0, transform.position.z);
        next = Vector3.Lerp(current, next, acceleration * Time.fixedDeltaTime);
        rb.AddForce(next, ForceMode.VelocityChange);

    }

    IEnumerator ChangeTrack()
    {
        isChangingTrack = true;

        while(isChangingTrack)
        {
            // Vector3 target = Vector3.right * speed * Time.fixedDeltaTime * direction;
            // rb.AddForce(target, ForceMode.VelocityChange);
            rb.MovePosition(new Vector3(points[index], transform.position.y, transform.position.z));
            Debug.Log("Changing Track");

            if(transform.position.x >= points[index])
            {
                isChangingTrack = false;
            }

            yield return Time.fixedDeltaTime;
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

        speed /= nitroBoost;
        maxSpeed /= nitroBoost;

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
}
