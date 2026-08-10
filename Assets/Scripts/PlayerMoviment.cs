using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(0, 0, speed);
        controller.Move(move * Time.deltaTime);
    }
}
