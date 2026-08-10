using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Locomotive", menuName = "Scriptable Objects/Locomotive")]
public class Locomotive : ScriptableObject
{
    [Header("Base stats")]
    public float nitro;
    public float rockets;
    public float drifting;
    public float fruits;
    public float coal_cost;
    public GameObject model_prefab;
}
