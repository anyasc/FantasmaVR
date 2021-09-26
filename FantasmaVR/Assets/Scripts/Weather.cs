using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public Material clear, noMoon;
    [SerializeField] GameObject mist, thunder;
    [SerializeField] RainSoundControl rainSound;

    private void Start()
    {
        RenderSettings.skybox = noMoon;
    }
    public void StopRain()
    {
        RenderSettings.skybox = clear;
        mist.SetActive(false);
        thunder.SetActive(false);
        rainSound.StopRain();
    }
}
