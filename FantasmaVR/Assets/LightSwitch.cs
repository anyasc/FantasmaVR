using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<GameObject> lights;

    private bool focus;

    public bool lightOn;


    private void Start()
    {

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
            FindObjectOfType<AudioManager>().Play("LightSwitch");
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
