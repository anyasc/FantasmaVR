using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    bool isOn;
    float delay;

    private void Start()
    {
        isOn = gameObject.GetComponent<Light>().enabled;
    }
    void Update()
    {
        if (isOn)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        isOn = false;
        int times = Random.Range(2, 5);

        for (int i = 0; i < times; i++)
        {
            gameObject.GetComponent<Light>().enabled = false;
            delay = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(delay);
            gameObject.GetComponent<Light>().enabled = true;
            delay = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(delay);
        }

        delay = Random.Range(5f, 10f);
        yield return new WaitForSeconds(delay);
        isOn = true;
    }
}
