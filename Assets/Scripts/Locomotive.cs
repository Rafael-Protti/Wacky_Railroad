using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Locomotive", menuName = "Scriptable Objects/Locomotive")]
public class Locomotive : ScriptableObject
{
    public float rocket;
    public float nitro;
    public float drift;
    public float cargo;
}
