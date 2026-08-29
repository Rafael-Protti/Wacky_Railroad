using UnityEngine;
using UnityEngine.Splines;

public class Ghost : MonoBehaviour
{
    public SplineAnimate splineAnimate;

    public float speed;

    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();

        speed = GameObject.Find("PlayerSetup").GetComponent<PlayerController>().speed;
        splineAnimate.Play();
        splineAnimate.MaxSpeed = speed;
    }

    void Update()
    {
        splineAnimate.MaxSpeed = speed;
    }
}
