using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public float speed = 0;
    public Material pink;
    Material originalMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);
    }

    public void StartRotation()
    {
        speed = 60;
        GetComponent<Renderer>().material = pink;
    }

    public void StopRotation()
    {
        speed = 0;
        GetComponent<Renderer>().material = originalMaterial;
    }

}
