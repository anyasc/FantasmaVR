using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSoundControl : MonoBehaviour
{
    FMOD.Studio.EventInstance Rain;

    // Start is called before the first frame update
    void Start()
    {
        Rain = FMODUnity.RuntimeManager.CreateInstance("event:/Rain");
        Rain.start();
    }


    public void GoInside(float x)
    {
        Rain.setParameterByName("Place", x);

    }

    public void StopRain()
    {
        Rain.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
