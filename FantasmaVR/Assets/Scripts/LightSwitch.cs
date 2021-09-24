using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<GameObject> lights;
    FMOD.Studio.EventInstance switchSound;



    private bool focus;

    public bool lightOn;


    private void Start()
    {
        switchSound = FMODUnity.RuntimeManager.CreateInstance("event:/LightSwitch");
            foreach (GameObject l in lights)
            {
            lightOn = l.activeSelf;
            }
    }
    void Update()
    {
        if (focus && Input.GetButtonDown("Z"))
        {
            lightOn = !lightOn;
            foreach (GameObject l in lights)
            {
                l.SetActive(lightOn);
            }
            int p = lightOn ? 0 : 1;
            switchSound.setParameterByName("OnOff", p);
            switchSound.start();
        }


    }

    public void PointerEnter()
    {
        focus = true;
    }

    public void PointerExit()
    {
        focus = false;
    }
}
