using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
public class PlayerController : MonoBehaviour
{
    public SplineContainer spline_left;
    public SplineContainer spline_middle;
    public SplineContainer spline_right;

    public List<SplineContainer> splines = new();
    public SplineAnimate splineAnimate;

    public float speed = 5f;

    public int currentTrack = 1;
    
    void Start()
    {
        // splines.Add(spline_left); splines.Add(spline_middle); splines.Add(spline_right);
        // splineAnimate = GetComponent<SplineAnimate>();
        // splineAnimate.Container = spline_middle;
        // splineAnimate.Play();
        // splineAnimate.MaxSpeed = speed;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     ChangeTrack(currentTrack--);
        // }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     ChangeTrack(currentTrack++);
        // }
    }

    void ChangeTrack(int index)
    {
        if(currentTrack > 2) currentTrack = 2;
        if(currentTrack < 0) currentTrack = 0;

        splineAnimate.Container = splines[index];
    }
}
