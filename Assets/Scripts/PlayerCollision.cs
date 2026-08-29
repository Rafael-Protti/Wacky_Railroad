using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }

    void CheckGround(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<LocomotiveMoviment>().isGrounded = true;
            Debug.Log("Grounded");
        }
    }
}
