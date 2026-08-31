using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        CheckGround(collision);     
    }

    void OnCollisionExit(Collision collision)
    {
        CheckAir(collision);
    }

    void CheckGround(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<LocomotiveMoviment>().OnGround();
            Debug.Log("Grounded");
        }
    }

    void CheckAir(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<LocomotiveMoviment>().OnAir();
            Debug.Log("On air");
        }
    }
}
