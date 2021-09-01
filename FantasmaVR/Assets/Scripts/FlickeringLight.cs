using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    bool isOn;
    public bool flicker = false;
    float delay;

    private void Start()
    {
        isOn = gameObject.GetComponent<Light>().enabled;
    }
    void Update()
    {
        if (isOn && flicker)
        {
            StartCoroutine(Flicker());
        }
    }

    public void lightSwitch()
    {
        if (isOn)
        {
            gameObject.GetComponent<Light>().enabled = false;
            isOn = false;
        }
        else
        {
            gameObject.GetComponent<Light>().enabled = true;
            isOn = true;
        }
    }

    IEnumerator Flicker()
    {
        isOn = false;
        gameObject.GetComponent<Light>().enabled = false;
        delay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<Light>().enabled = true;
        delay = Random.Range(0.3f, 1f);
        yield return new WaitForSeconds(delay);
        isOn = true;
    }
}
