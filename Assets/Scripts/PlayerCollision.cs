using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision);
    }

    void OnTriggerEnter(Collider other)
    {
        GhostCheck(other);
        CheckObstacles(other);
    }

    void CheckGround(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<LocomotiveMoviment>().OnGround();
            Debug.Log("Grounded");
        }
    }

    void GhostCheck(Collider other)
    {
        if(other.gameObject.CompareTag("Ghost"))
        {
            GetComponent<LocomotiveMoviment>().isChangingTrack = false;
        }
    }

    void CheckObstacles(Collider other)
    {
        if(other.gameObject.CompareTag("Light"))
        {
            if(!GetComponent<LocomotiveMoviment>().isDrifting)
            {
                CargoDrop();
            }
        }

        if(other.gameObject.CompareTag("Low") || other.gameObject.CompareTag("High"))
        {
            CargoDrop();
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            if(!GetComponent<LocomotiveMoviment>().isNitro)
            {
                CargoDrop();
            }
        }
    }

    void CargoDrop()
    {
        GetComponent<LocomotiveResources>().SetCargoValue(-1);
        Debug.Log("CargoDrop");
    }
}
