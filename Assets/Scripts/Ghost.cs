using UnityEngine;
using UnityEngine.Splines;

public class Ghost : MonoBehaviour
{
    public SplineAnimate splineAnimate;

    float speed = 1;
    float currentTime;

    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        splineAnimate.Play();
        splineAnimate.MaxSpeed = speed;
    }

    public void ChangeSpeed(float speedMultiplier)
    {
        GetTime();
        speed *= speedMultiplier;
        splineAnimate.MaxSpeed = speed;
        SetTime();
    }

    void GetTime()
    {
        currentTime = splineAnimate.NormalizedTime;
    }

    void SetTime()
    {
        splineAnimate.NormalizedTime = currentTime;
    }
}
