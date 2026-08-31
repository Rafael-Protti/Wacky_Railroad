using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LocomotiveResources : MonoBehaviour
{
    public Locomotive locomotive;
    public Slider sliderRocket;
    public Slider sliderNitro;
    public Slider sliderDrift;
    public Slider sliderCargo;
    float rocket;
    float nitro;
    float drift;
    float cargo;

    void Start()
    {
        SetMaxValues();
    }

    void SetMaxValues()
    {
        rocket = locomotive.rocket;
        nitro = locomotive.nitro;
        drift = locomotive.drift;
        cargo = locomotive.cargo;

        sliderRocket.maxValue = rocket;
        sliderNitro.maxValue = nitro;
        sliderDrift.maxValue = drift;
        sliderCargo.maxValue = cargo;

        UpdateValues();
    }

    void UpdateValues()
    {
        sliderRocket.value = rocket;
        sliderNitro.value = nitro;
        sliderDrift.value = drift;
        sliderCargo.value = cargo;
    }

    float ValidateValue(float resource,  float maxResource, float value)
    {
        resource += value;

        if (resource < 0)
        {
            return 0;
        }

        if (resource > maxResource)
        {
            return maxResource;
        }

        return resource;
    }

    public void SetRocketValue(float value)
    {
        rocket = ValidateValue(rocket, locomotive.rocket, value);
        UpdateValues();
    }

    public void SetNitroValue(float value)
    {
        nitro = ValidateValue(nitro, locomotive.nitro, value);
        UpdateValues();
    }

    public void SetDrifValue(float value)
    {
        drift = ValidateValue(drift, locomotive.drift, value);
        UpdateValues();
    }

    public void SetCargoValue(float value)
    {
        cargo = ValidateValue(cargo, locomotive.cargo, value);
        UpdateValues();
    }

    public bool RocketAvaliable()
    {
        if(rocket == 0) return false;
        return true;
    }
}
