using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    public int coal_currency;
    public Locomotive chosen_locomotive;
    void Start()
    {
        if(chosen_locomotive == null)
        {
            Debug.Log("Insira o scriptable object da lomotiva");
        }
    }

    void Update()
    {
        
    }
}
