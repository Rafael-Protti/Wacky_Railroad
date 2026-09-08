using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckGhost(other);
    }

    void CheckGround(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<LocomotiveMoviment>().OnGround();
            Debug.Log("Grounded");
        }
    }

    // void CheckAir(Collision collision)
    // {
    //     if(collision.gameObject.CompareTag("Ground"))
    //     {
    //         GetComponent<LocomotiveMoviment>().OnAir();
    //         Debug.Log("On air");
    //     }
    // }

    void CheckGhost(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ghost"))
        {
            GetComponent<LocomotiveMoviment>().isChangingTrack = false;
            Debug.Log("Ghost collided");
        }
    }
}
