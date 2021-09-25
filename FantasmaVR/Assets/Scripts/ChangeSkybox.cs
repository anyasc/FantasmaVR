using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public Material clear, noMoon;

    private void Start()
    {
        RenderSettings.skybox = noMoon;
    }
    public void StopRain()
    {
        RenderSettings.skybox = clear;
    }
}
