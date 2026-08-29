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
            GetComponent<LocomotiveMoviment>().isGrounded = true;
            Debug.Log("Grounded");
        }
    }

    void CheckGhost(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ghost"))
        {
            GetComponent<LocomotiveMoviment>().ghostCollided = true;
            Debug.Log("Ghost collided");
        }
    }
}
