using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private Light lightning;
    FMOD.Studio.EventInstance Thunder;
    bool isOn = true;
    float delay;

    private void Start()
    {
        lightning = GetComponent<Light>();
        Thunder = FMODUnity.RuntimeManager.CreateInstance("event:/Lightning");
    }
    void Update()
    {
        if (isOn)
        {
            StartCoroutine(Flicker());
        }
    }

    public void GoInside()
    {
        Thunder.setParameterByName("Inside", 1);
    }
    IEnumerator Flicker()
    {
        isOn = false;
        int times = Random.Range(1, 4);
        transform.rotation = Quaternion.Euler(Random.Range(30f, 60f), Random.Range(0f, 360f), 0);
        float intensity = Random.Range(0.5f, 2.5f);
        GetComponent<Light>().intensity = intensity;
        float volume = intensity / intensity * Random.Range(0.8f, 1f);
        Thunder.setParameterByName("Pitch", Random.Range(0f, 1f));
        Thunder.setParameterByName("Volume", volume);

        delay = Random.Range(30f, 70f);
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < times; i++)
        {
            lightning.enabled = true;
            delay = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(delay);
            lightning.enabled = false;
            delay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(delay);
        }
        delay = Random.Range(0f, 2f);
        yield return new WaitForSeconds(delay);

        Thunder.start();



        isOn = true;
    }

}
