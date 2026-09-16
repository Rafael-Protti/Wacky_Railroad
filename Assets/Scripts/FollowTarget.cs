using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float suavidade = 5; //suavidade do movimento da câmera. "Smooth".

    void Start()
    {
        if (target == null)
        {
            Debug.Log("Adicione o player ao Inspector");
        }
    }

    void LateUpdate()
    {
        Vector3 novaPosicao = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, novaPosicao, suavidade * Time.deltaTime);
    }
}
