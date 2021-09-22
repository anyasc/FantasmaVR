using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSoundControl : MonoBehaviour
{
    FMOD.Studio.EventInstance Rain;

    public bool testEnter = false;
    public bool testExit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Rain = FMODUnity.RuntimeManager.CreateInstance("event:/Rain");
        Rain.start();
    }

    public void EnterHouse()
    {
        Rain.setParameterByName("Place", 1f);
    }

    public void Leave()
    {
        Rain.setParameterByName("Place", 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (testEnter)
        {
            EnterHouse();
            testEnter = false;
        }
        if (testExit)
        {
            Leave();
            testExit = false;
        }
    }

    public void GoInside(float x)
    {
        Rain.setParameterByName("Place", x);

    }
}
